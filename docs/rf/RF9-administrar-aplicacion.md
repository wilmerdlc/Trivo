#### RF9. Administrar aplicación
Panel de administración exclusivo para usuarios con permisos de staff, que permite asignar y gestionar roles administrativos con distintos niveles de acceso, visualizar reportes realizados por la comunidad y aplicar sanciones (advertencias, suspensiones temporales o baneo definitivo) a cuentas que incumplan las políticas de la plataforma, quedando registradas en un historial de acciones administrativas.

**Requerimientos funcionales**

*   **RF1:** Reportes y listados administrativos en PDF (paginados). Todas las entidades listadas deben incluir el campo de fecha de creación (`CreatedAt`, ya presente en el modelo base de todas las entidades).
    *   **Usuarios registrados:** endpoint paginado. — *Implementado* (`GetLatestUsersPagedQueryHandler`).
    *   **Emparejamientos realizados:** endpoint paginado. — *Implementado* (`GetLatestMatchesQueryHandler`).
    *   **Reportes realizados:** endpoint paginado, más un endpoint adicional para los últimos 10. — *Parcialmente implementado* (ya existe consulta paginada de reportes; falta confirmar el endpoint de "últimos 10").
    *   **Usuarios baneados:** endpoint de últimos 10 ya existe; falta agregar soporte de paginación al endpoint actual (`/api/v{version}/admin/banned-users`). — *Pendiente la paginación*.
    *   **Reclutadores registrados:** endpoint paginado. — *Pendiente a implementar*.
    *   **Expertos registrados:** endpoint paginado. — *Pendiente a implementar*.

*   **RF2:** Gestión y resolución de reportes.
    > **Estado:** Pendiente a implementar.
    *   El usuario puede reportar un **perfil** o un **mensaje** (texto, imagen o archivo).
    *   El reporte debe incluir: ID del usuario que reporta, ID del usuario reportado, tipo de reporte (mensaje/perfil), notas del reporte (texto libre) y el contenido exacto reportado.
    *   **Flujo administrativo:**
        *   El administrador consulta el detalle del reporte, incluyendo la información completa (DTO) de ambos usuarios involucrados.
        *   El administrador **aprueba** o **rechaza** el reporte.
            *   Si se rechaza: se marca como *rechazado* y se notifica al usuario que reportó.
            *   Si se aprueba: el administrador define el **tiempo de la sanción**.
        *   En ambos casos, se registra un campo obligatorio de **razón final**, explicando la decisión.
    *   **Notificación al usuario sancionado:**
        *   Notificación opcional por correo al aplicarse la sanción.
        *   Al iniciar sesión, si el usuario está bloqueado, el sistema debe devolver el **tiempo restante** de la sanción y la **razón final**.
    *   **Endpoints requeridos:**
        *   Consultar reporte por ID (con detalle completo de ambos usuarios).
        *   Actualizar reporte (estado, razón final y duración de sanción si aplica).
        *   Enviar nuevo reporte.
        *   Actualizar el endpoint de login para validar bloqueo activo y devolver tiempo restante + razón.
