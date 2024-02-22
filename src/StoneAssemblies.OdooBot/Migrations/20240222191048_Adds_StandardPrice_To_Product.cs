using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneAssemblies.OdooBot.Migrations
{
    /// <inheritdoc />
    public partial class Adds_StandardPrice_To_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "StandardPrice",
                table: "Products",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandardPrice",
                table: "Products");
        }
    }
}
