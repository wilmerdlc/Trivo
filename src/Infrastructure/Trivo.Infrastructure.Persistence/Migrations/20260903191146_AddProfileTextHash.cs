using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trivo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileTextHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileTextHash",
                table: "User",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileTextHash",
                table: "User");
        }
    }
}
