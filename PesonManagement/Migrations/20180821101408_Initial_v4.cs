using Microsoft.EntityFrameworkCore.Migrations;

namespace PesonManagement.Migrations
{
    public partial class Initial_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconCss",
                table: "Functions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconCss",
                table: "Functions");
        }
    }
}
