using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class paskutinis3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quote",
                value: "Infernal Vel'Koz is a terrifying creature of pure elemental energy, a being of immense power and destruction. He is a force of nature, a being of pure chaos and destruction, who seeks only to consume and destroy all that stands in his way.");

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ChromaPrices", "Quote" },
                values: new object[] { "290;290;Loot Exclusive;290;290;Bundle Exclusive;290;290;290", "Battle Principal Yuumi is the headmistress of the prestigious Prancetown School for Magical Cats, where she teaches young yordles the arcane arts. She is a stern but caring teacher, who is always willing to go the extra mile to help her students succeed." });

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 4,
                column: "Quote",
                value: "The Lunar Empress is a figure of myth and legend, a celestial being who descends from the heavens to protect the world from the forces of darkness. She is a beacon of hope, a symbol of the power of the moon, and a guardian of the stars.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quote",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ChromaPrices", "Quote" },
                values: new object[] { "290;290;Loot Exclusive;290;290;Bundle Exclusive;290;290", null });

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 4,
                column: "Quote",
                value: null);
        }
    }
}
