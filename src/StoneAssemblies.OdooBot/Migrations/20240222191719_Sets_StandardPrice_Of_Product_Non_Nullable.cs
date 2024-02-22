using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneAssemblies.OdooBot.Migrations
{
    /// <inheritdoc />
    public partial class Sets_StandardPrice_Of_Product_Non_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "StandardPrice",
                table: "Products",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "StandardPrice",
                table: "Products",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
