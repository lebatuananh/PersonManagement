using Microsoft.EntityFrameworkCore.Migrations;

namespace PesonManagement.Migrations
{
    public partial class Initial_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blance",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Persons");

            migrationBuilder.AddColumn<decimal>(
                name: "Blance",
                table: "AppUsers",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
