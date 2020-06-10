using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgencyDatabaseImplement.Migrations
{
    public partial class FixGuide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuideName",
                table: "Guides");

            migrationBuilder.AddColumn<string>(
                name: "GuideThemeName",
                table: "Guides",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuideThemeName",
                table: "Guides");

            migrationBuilder.AddColumn<string>(
                name: "GuideName",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
