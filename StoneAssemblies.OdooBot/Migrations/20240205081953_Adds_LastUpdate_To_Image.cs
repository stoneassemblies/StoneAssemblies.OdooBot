using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneAssemblies.OdooBot.Migrations
{
    /// <inheritdoc />
    public partial class Adds_LastUpdate_To_Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Images",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Images");
        }
    }
}
