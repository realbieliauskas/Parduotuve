using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class Cleanupseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Chromas",
                newName: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Chromas",
                newName: "URL");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role", "Username" },
                values: new object[] { 1, "name@domain.com", "password", 1, "name" });
        }
    }
}
