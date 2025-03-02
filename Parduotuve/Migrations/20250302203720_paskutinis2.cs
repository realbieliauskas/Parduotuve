using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class paskutinis2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChromaPrices",
                value: "290;290;Loot Exclusive;290;290;Bundle Exclusive");

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChromaPrices",
                value: "290;290;Loot Exclusive;290;290;Bundle Exclusive");

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 3,
                column: "ChromaPrices",
                value: "290;290;Loot Exclusive;290;290;Bundle Exclusive;290;290");

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 4,
                column: "ChromaPrices",
                value: "290;290;Loot Exclusive;290;Bundle Exclusive");

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 5,
                column: "ChromaPrices",
                value: "290;290;Loot Exclusive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChromaPrices",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChromaPrices",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 3,
                column: "ChromaPrices",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 4,
                column: "ChromaPrices",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 5,
                column: "ChromaPrices",
                value: null);
        }
    }
}
