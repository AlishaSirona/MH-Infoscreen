using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbInfoscreenLibrary.Migrations
{
    public partial class RenameOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Pages",
                newName: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Pages",
                newName: "Position");
        }
    }
}
