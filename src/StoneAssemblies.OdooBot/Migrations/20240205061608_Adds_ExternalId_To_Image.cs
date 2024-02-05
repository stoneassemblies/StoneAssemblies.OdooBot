using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneAssemblies.OdooBot.Migrations
{
    /// <inheritdoc />
    public partial class Adds_ExternalId_To_Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExternalId",
                table: "Images",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ExternalId",
                table: "Images",
                column: "ExternalId",
                unique: true,
                filter: "ExternalId IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ExternalId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Images");
        }
    }
}
