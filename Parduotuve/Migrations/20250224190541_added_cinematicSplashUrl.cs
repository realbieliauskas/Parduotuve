﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class added_cinematicSplashUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CinematicSplashUrl",
                table: "Skins",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                column: "CinematicSplashUrl",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CinematicSplashUrl",
                table: "Skins");
        }
    }
}
