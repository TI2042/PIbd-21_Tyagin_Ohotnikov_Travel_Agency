using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgencyDatabaseImplement.Migrations
{
    public partial class _25052020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Suppliers_SupplierId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestGuides_Requests_RequestId",
                table: "RequestGuides");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DateImplement",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "HotelGuides");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "RequestGuides",
                newName: "RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_RequestGuides_RequestId",
                table: "RequestGuides",
                newName: "IX_RequestGuides_RequestID");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Hotels",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Reserved",
                table: "HotelGuides",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Suppliers_SupplierId",
                table: "Hotels",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGuides_Requests_RequestID",
                table: "RequestGuides",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Suppliers_SupplierId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestGuides_Requests_RequestID",
                table: "RequestGuides");

            migrationBuilder.DropColumn(
                name: "Reserved",
                table: "HotelGuides");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "RequestGuides",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestGuides_RequestID",
                table: "RequestGuides",
                newName: "IX_RequestGuides_RequestId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateImplement",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Sum",
                table: "Requests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Hotels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "IsReserved",
                table: "HotelGuides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Suppliers_SupplierId",
                table: "Hotels",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGuides_Requests_RequestId",
                table: "RequestGuides",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
