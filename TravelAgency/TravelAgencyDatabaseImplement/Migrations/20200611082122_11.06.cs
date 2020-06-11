using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgencyDatabaseImplement.Migrations
{
    public partial class _1106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Sum",
                table: "Requests",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Requests");
        }
    }
}
