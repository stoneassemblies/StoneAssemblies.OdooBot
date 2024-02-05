using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneAssemblies.OdooBot.Migrations
{
    /// <inheritdoc />
    public partial class Fixes_Unique_Index_In_Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ExternalId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Size_ExternalId",
                table: "Images",
                columns: new[] { "Size", "ExternalId" },
                unique: true,
                filter: "ExternalId IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_Size_ExternalId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ExternalId",
                table: "Images",
                column: "ExternalId",
                unique: true,
                filter: "ExternalId IS NOT NULL");
        }
    }
}
