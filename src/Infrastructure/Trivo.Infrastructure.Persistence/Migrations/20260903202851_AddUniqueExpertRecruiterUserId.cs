using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trivo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueExpertRecruiterUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recruiter_FKUserId",
                table: "Recruiter");

            migrationBuilder.DropIndex(
                name: "IX_Expert_FKUserId",
                table: "Expert");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiter_FKUserId",
                table: "Recruiter",
                column: "FKUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expert_FKUserId",
                table: "Expert",
                column: "FKUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recruiter_FKUserId",
                table: "Recruiter");

            migrationBuilder.DropIndex(
                name: "IX_Expert_FKUserId",
                table: "Expert");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiter_FKUserId",
                table: "Recruiter",
                column: "FKUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expert_FKUserId",
                table: "Expert",
                column: "FKUserId");
        }
    }
}
