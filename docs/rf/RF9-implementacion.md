# RF9. Administrar aplicación — Documentación de implementación

> Este documento describe **cómo quedó implementado** RF1 y RF2 de RF9 (ver [RF9-administrar-aplicacion.md](RF9-administrar-aplicacion.md) para el enunciado original de los requerimientos). Todo lo aquí descrito corresponde a código real ya escrito en el repositorio, no a un plan.

## 1. Resumen

Se implementaron los dos requerimientos que estaban pendientes:

- **RF1** — listados administrativos paginados en JSON (el frontend arma el PDF con esos datos), con fecha de creación en cada entidad.
- **RF2** — flujo completo de reporte de perfiles/mensajes, revisión administrativa, sanciones (advertencia, suspensión temporal, baneo definitivo) y bloqueo de acceso al iniciar sesión.

La API sigue devolviendo JSON plano en todos los casos. No se generan PDFs en el backend.

## 2. Modelo de dominio nuevo

### 2.1 Enums

| Enum | Valores | Uso |
|---|---|---|
| `ReportType` | `Message`, `Profile` | Qué se está reportando. |
| `ReportStatus` | `Pending`, `Approved`, `Rejected` (+ `Resolved`, legado, ya no se asigna) | Estado del reporte. |
| `ReportDecision` | `Approved`, `Rejected` | Decisión que toma el administrador al resolver. |
| `SanctionType` | `Warning`, `TemporarySuspension`, `PermanentBan` | Tipo de sanción aplicada. |
| `UserStatus` | `Banned`, `Active`, `Inactive`, `Suspended` (nuevo) | Estado de la cuenta del usuario. |

### 2.2 Entidad `Report` (rediseñada)

Antes solo soportaba reportes de mensaje y no distinguía "quién reportó" de "quién fue reportado" de forma explícita. Ahora:

```csharp
public sealed class Report
{
    public Guid? ReportId { get; set; }
    public Guid? ReportedById { get; set; }      // quién reportó
    public Guid? ReportedUserId { get; set; }     // a quién se reportó
    public string? ReportType { get; set; }       // Message | Profile
    public Guid? MessageId { get; set; }          // solo si ReportType == Message
    public string? ReportStatus { get; set; }
    public string? Note { get; set; }
    public string? ReportedContent { get; set; }      // snapshot del contenido al momento del reporte
    public string? ReportedContentType { get; set; }  // Text | Image | File (solo mensajes)
    public string? FinalReason { get; set; }          // razón obligatoria del admin al resolver
    public DateTime CreatedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public Guid? ReviewedByAdminId { get; set; }
    // navegación: Message, Reporter, ReportedUser, ReviewedByAdmin, Sanction
}
```

`ReportedContent` guarda una copia del contenido exacto reportado (texto del mensaje, o un snapshot de campos del perfil) en el momento del reporte, para que la evidencia sobreviva aunque el usuario reportado edite o borre el mensaje/perfil después.

### 2.3 Entidad `Sanction` (nueva)

Es, a la vez, la sanción aplicada **y** el historial de acciones administrativas que pide el enunciado de RF9 — no se borran filas, solo se marcan como revocadas.

```csharp
public sealed class Sanction : BaseEntity
{
    public Guid? UserId { get; set; }
    public Guid? ReportId { get; set; }      // el reporte que la originó, si aplica
    public Guid? AdminId { get; set; }       // quién la aplicó
    public string? Type { get; set; }        // SanctionType
    public string? Reason { get; set; }
    public DateTime? ExpiresAt { get; set; } // solo para TemporarySuspension
    public DateTime? RevokedAt { get; set; } // seteado si un admin la levanta antes de tiempo
}
```

### 2.4 Migración de base de datos

`20260921003755_AddReportSanctions`:
- Crea la tabla `Sanction`.
- Agrega a `Report`: `CreatedAt`, `ReportType`, `FKReportedUserId`, `FinalReason`, `ReportedContent`, `ReportedContentType`, `ReviewedAt`, `FKReviewedByAdminId`.
- Elimina la columna `UserId` de `Report` (era un FK "fantasma" hacia `User` que nunca se llenaba — por eso `count/users-report` siempre daba 0).
- **Backfill de filas existentes**: para cada reporte ya guardado, calcula `FKReportedUserId`, `ReportedContent`, `ReportedContentType` y `CreatedAt` a partir del mensaje asociado (`FKMessageId`), antes de volver esas columnas `NOT NULL`. Escrita a mano sobre la migración autogenerada porque el escenario tenía datos preexistentes.

## 3. Reglas de negocio

### 3.1 Crear un reporte (`CreateReportCommandHandler`)

- Un reporte de **mensaje** exige que quien reporta sea el **receptor** del mensaje (no se puede reportar el propio mensaje ni uno ajeno que no llegó a esa cuenta).
- Un reporte de **perfil** exige `reportedUserId` y no puede llevar `messageId`. No se puede reportar a uno mismo.
- No se permite tener dos reportes **pendientes** sobre el mismo objetivo (mismo mensaje, o mismo usuario reportado en un reporte de perfil) — evita spam de reportes duplicados mientras el primero sigue sin resolver.
- El comando implementa `IUserOwnedRequest` sobre `ReportedById`: solo el usuario autenticado puede reportar en su propio nombre.

### 3.2 Resolver un reporte (`ResolveReportCommandHandler`)

- Solo se puede resolver un reporte en estado `Pending` (409 si ya se resolvió).
- **Razón final obligatoria** en ambos casos (aprobar o rechazar).
- **Rechazar**: marca el reporte como `Rejected` y notifica (in-app) al usuario que reportó, indicando que no procedió.
- **Aprobar**: exige `sanctionType`. Si es `TemporarySuspension`, exige `durationDays` (1–365). Crea la fila en `Sanction` y escala el estado de la cuenta:
  - `PermanentBan` → `User.UserStatus = Banned`.
  - `TemporarySuspension` → `User.UserStatus = Suspended`, **salvo que el usuario ya esté baneado** (una suspensión nunca degrada un baneo).
  - `Warning` → no cambia el estado de la cuenta, solo queda en el historial.
- El usuario sancionado recibe una notificación in-app siempre, y un correo si `notifyByEmail` viene en `true` (por defecto `true`).
- Un fallo al enviar la notificación/correo se registra en el log pero **no revierte** la resolución ya guardada — la decisión administrativa ya es un hecho consumado en base de datos.

### 3.3 Bloqueo de acceso (`IAccountAccessService`)

Nuevo servicio, usado tanto en **login** como en **refresh-token** (antes el refresh-token no validaba nada de esto, por lo que una sesión ya emitida sobrevivía a una sanción).

- Se evalúa **después** de validar la contraseña — el motivo de la sanción es información privada del dueño de la cuenta, no algo que deba poder obtener cualquiera que solo conozca el email.
- Si la cuenta está `Banned` o `Suspended`, busca la sanción vigente (`ISanctionRepository.GetCurrentBlockAsync`) y arma un error `409` con datos estructurados:
  - `errorCode`: `Account.Suspended` o `Account.Banned`
  - `reason`, `expiresAt`, `remainingSeconds`, `sanctionType`
- Si la cuenta figura `Suspended` pero la sanción ya venció, la levanta automáticamente (`UserStatus → Active`) y deja continuar el login.

Para soportar esto se extendió `Error` con `Extensions` (diccionario libre) y `ProblemDetailsMapper` los copia al cuerpo de la respuesta.

### 3.4 Un solo camino para sancionar

Se eliminó el endpoint legado `PUT /admin/users/{userId}/ban` (y el `BanUserCommand`/`BanAsync` que lo respaldaban). **La única forma de banear, suspender o advertir a un usuario es resolviendo un reporte** (`PUT /admin/reports/{reportId}`, §4.2) — así toda sanción queda siempre con razón, tipo, duración y administrador responsable en `Sanction`, sin excepción.

La contraparte — **levantar** una sanción activa (ban o suspensión) — sigue siendo `PUT /admin/users/{userId}/unban`. No es "otra forma de sancionar", es la acción inversa, y ya funciona sin importar si la sanción se originó por un reporte: `UnbanUserCommandHandler` restaura `UserStatus = Active` y revoca (`RevokedAt`) cualquier sanción activa del usuario en `Sanction`.

### 3.5 Otros ajustes de consistencia

- `CreateMatchingCommandHandler`: ahora también rechaza crear un match si cualquiera de las dos partes está `Suspended` (antes solo miraba `Banned`).
- Se eliminó `GetPagedLatestReportsAsync` de `IAdministratorRepository` (quedó sin uso una vez que `IReportRepository.GetPagedForAdminAsync` cubrió `/admin/reports`) y se corrigió `GetReportedCountAsync`, que contaba por un FK (`Report.User`) que nunca se llenaba — por eso `count/users-report` siempre daba 0. Ahora cuenta por `ReportedUserId`.

## 4. Endpoints

Todos bajo `[Authorize(Roles = "Administrator")]` salvo que se indique lo contrario. Todas las listas paginadas devuelven `PagedResult<T>` (`items`, `totalItems`, `currentPage`, `totalPages`).

### 4.1 RF1 — Listados administrativos

| Endpoint | Método | Descripción |
|---|---|---|
| `/admin/last-user` | GET | Usuarios registrados, paginado. *(ya existía; se le agregó `createdAt`)* |
| `/admin/last-match` | GET | Emparejamientos, paginado. *(ya existía)* |
| `/admin/reports` | GET | Reportes, paginado. Filtro opcional `status` (Pending/Approved/Rejected). **Nuevo.** |
| `/admin/reports/latest` | GET | Últimos 10 reportes. **Nuevo.** |
| `/admin/banned-users` | GET | Últimos 10 baneados. *(ya existía, sin paginar — se dejó igual para no romper al frontend)* |
| `/admin/banned-users/paged` | GET | Usuarios baneados, paginado. **Nuevo.** |
| `/admin/recruiters` | GET | Reclutadores registrados, paginado. **Nuevo.** |
| `/admin/experts` | GET | Expertos registrados, paginado. **Nuevo.** |

### 4.2 RF2 — Reportes y sanciones

| Endpoint | Método | Auth | Descripción |
|---|---|---|---|
| `/report` | POST | Usuario autenticado | Crea un reporte de mensaje o de perfil. |
| `/admin/reports/{reportId}` | GET | Admin | Detalle completo del reporte, con el perfil completo de ambos usuarios. |
| `/admin/reports/{reportId}` | PUT | Admin | **Único camino para sancionar** — aprueba (con sanción) o rechaza el reporte (ver §3.2 y §3.4). |
| `/admin/users/{userId}/unban` | PUT | Admin | Levanta cualquier sanción activa del usuario (ban o suspensión) y restaura `UserStatus = Active`. |
| `/admin/sanctions` | GET | Admin | Historial de sanciones, paginado. Filtro opcional `userId`. |

**`POST /report`** — body:
```json
{
  "reportedById": "guid",
  "messageId": "guid | null",
  "note": "texto libre, máx 250 caracteres",
  "reportedUserId": "guid | null",
  "reportType": "Message | Profile | null"
}
```
Si `reportType` se omite y viene `messageId`, se asume `Message` (compatibilidad con el payload original).

**`PUT /admin/reports/{reportId}`** — body:
```json
{
  "decision": "Approved | Rejected",
  "finalReason": "texto, máx 1000 caracteres",
  "sanctionType": "Warning | TemporarySuspension | PermanentBan | null",
  "durationDays": "1-365, solo si sanctionType es TemporarySuspension",
  "notifyByEmail": true
}
```

**Login / refresh-token bloqueados** — respuesta 409 (ejemplo suspensión):
```json
{
  "status": 409,
  "detail": "Your account is suspended until 2026-10-01 12:00 UTC (9 days, 3 hours remaining). Reason: ...",
  "errorCode": "Account.Suspended",
  "sanctionType": "TemporarySuspension",
  "reason": "...",
  "expiresAt": "2026-10-01T12:00:00Z",
  "remainingSeconds": 788400
}
```

## 5. Decisiones de diseño

- **`/admin/banned-users` se dejó intacto** (sin paginar) y se agregó `/admin/banned-users/paged` aparte, en vez de modificar el contrato existente — paginar el original habría cambiado su forma de respuesta de arreglo a objeto paginado, rompiendo a quien ya lo consume.
- **El estado de la cuenta solo escala, nunca degrada**: aprobar una suspensión sobre un usuario ya baneado no lo "mejora" a suspendido.
- **Fallos de notificación no revierten la resolución**: una vez que el reporte se guardó como aprobado/rechazado, un error de correo o notificación in-app solo se loguea.
- **El motivo de la sanción se revela solo tras validar la contraseña** en login, para no filtrar información de la cuenta a quien no la posee.

## 6. Limitaciones conocidas / fuera de alcance

- **No se implementó** la asignación de roles administrativos con distintos niveles de acceso que menciona la introducción de RF9 — solo se cubrieron RF1 y RF2 tal como se pidió.
- Un usuario con un JWT ya emitido antes de ser sancionado conserva acceso hasta que ese token expire; el bloqueo se aplica en login y en refresh-token, no en cada request individual.
- **Sancionar requiere que exista un reporte previo.** Como se decidió un solo camino (§3.4), un administrador no puede banear/suspender/advertir a un usuario "de la nada" — necesita resolver un reporte sobre él. `POST /report` solo lo puede crear un `User` autenticado (`IUserOwnedRequest` valida contra el token del usuario, no del administrador — `Administrator` es una entidad separada, sin fila en `User`), así que hoy un administrador no tiene forma de generar ese reporte inicial en nombre propio si detecta el problema por fuera de la plataforma. Si esto hace falta, la solución es agregar un modo "reporte administrativo" a `CreateReportCommand` (o un command aparte) que no dependa de `IUserOwnedRequest`.
- No se generan PDFs en el backend: todos los endpoints de RF1 devuelven JSON; la maquetación a PDF es responsabilidad del frontend.
