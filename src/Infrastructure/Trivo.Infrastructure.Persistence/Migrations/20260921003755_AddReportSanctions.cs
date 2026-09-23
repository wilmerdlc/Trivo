using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trivo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReportSanctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Message_FKMessageId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_User_UserId",
                table: "Report");

            // The old "UserId" column was an accidental shadow foreign key (Report.User) that was
            // never populated. Drop it rather than repurposing it for the reviewing admin.
            migrationBuilder.DropIndex(
                name: "IX_Report_UserId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Report");

            migrationBuilder.AddColumn<Guid>(
                name: "FKReviewedByAdminId",
                table: "Report",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_FKReviewedByAdminId",
                table: "Report",
                column: "FKReviewedByAdminId");

            migrationBuilder.AlterColumn<Guid>(
                name: "FKMessageId",
                table: "Report",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            // New required columns are added with a temporary default so existing rows get valid
            // values, backfilled below, and the default is dropped again at the end.
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Report",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<string>(
                name: "ReportType",
                table: "Report",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "Message");

            // Nullable for now: existing reports don't know their reported user yet.
            migrationBuilder.AddColumn<Guid>(
                name: "FKReportedUserId",
                table: "Report",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinalReason",
                table: "Report",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportedContent",
                table: "Report",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportedContentType",
                table: "Report",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "Report",
                type: "timestamp with time zone",
                nullable: true);

            // Backfill existing (message) reports from the message they point to: the reported
            // user is the other participant, and the content/date are copied as the evidence.
            migrationBuilder.Sql(@"
                UPDATE ""Report"" r
                SET ""FKReportedUserId"" = CASE
                        WHEN m.""FKSenderId"" = r.""FKReportedById"" THEN m.""FKReceiverId""
                        ELSE m.""FKSenderId""
                    END,
                    ""ReportedContent"" = m.""Content"",
                    ""ReportedContentType"" = m.""Type"",
                    ""CreatedAt"" = COALESCE(m.""CreatedAt"", now())
                FROM ""Message"" m
                WHERE m.""MessageId"" = r.""FKMessageId"";");

            migrationBuilder.AlterColumn<Guid>(
                name: "FKReportedUserId",
                table: "Report",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Report",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<string>(
                name: "ReportType",
                table: "Report",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldDefaultValue: "Message");

            migrationBuilder.CreateTable(
                name: "Sanction",
                columns: table => new
                {
                    PKSanctionId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKReportId = table.Column<Guid>(type: "uuid", nullable: true),
                    FKAdminId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<string>(type: "varchar(50)", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RevokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKSanctionId", x => x.PKSanctionId);
                    table.ForeignKey(
                        name: "FK_Sanction_Administrator_FKAdminId",
                        column: x => x.FKAdminId,
                        principalTable: "Administrator",
                        principalColumn: "PKAdministratorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sanction_Report_FKReportId",
                        column: x => x.FKReportId,
                        principalTable: "Report",
                        principalColumn: "PKReportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sanction_User_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_FKReportedById",
                table: "Report",
                column: "FKReportedById");

            migrationBuilder.CreateIndex(
                name: "IX_Report_FKReportedUserId",
                table: "Report",
                column: "FKReportedUserId");

            migrationBuilder.CreateIndex(
                name: "IXReportCreatedAt",
                table: "Report",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IXReportStatus",
                table: "Report",
                column: "ReportStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Sanction_FKAdminId",
                table: "Sanction",
                column: "FKAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanction_FKReportId",
                table: "Sanction",
                column: "FKReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IXSanctionUserExpiresAt",
                table: "Sanction",
                columns: new[] { "FKUserId", "ExpiresAt" });

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Administrator_FKReviewedByAdminId",
                table: "Report",
                column: "FKReviewedByAdminId",
                principalTable: "Administrator",
                principalColumn: "PKAdministratorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Message_FKMessageId",
                table: "Report",
                column: "FKMessageId",
                principalTable: "Message",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_User_FKReportedById",
                table: "Report",
                column: "FKReportedById",
                principalTable: "User",
                principalColumn: "PKUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_User_FKReportedUserId",
                table: "Report",
                column: "FKReportedUserId",
                principalTable: "User",
                principalColumn: "PKUserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Administrator_FKReviewedByAdminId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Message_FKMessageId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_User_FKReportedById",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_User_FKReportedUserId",
                table: "Report");

            migrationBuilder.DropTable(
                name: "Sanction");

            migrationBuilder.DropIndex(
                name: "IX_Report_FKReportedById",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_FKReportedUserId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IXReportCreatedAt",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IXReportStatus",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "FKReportedUserId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "FinalReason",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ReportedContent",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ReportedContentType",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_FKReviewedByAdminId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "FKReviewedByAdminId",
                table: "Report");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Report",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_UserId",
                table: "Report",
                column: "UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "FKMessageId",
                table: "Report",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Message_FKMessageId",
                table: "Report",
                column: "FKMessageId",
                principalTable: "Message",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_User_UserId",
                table: "Report",
                column: "UserId",
                principalTable: "User",
                principalColumn: "PKUserId");
        }
    }
}
