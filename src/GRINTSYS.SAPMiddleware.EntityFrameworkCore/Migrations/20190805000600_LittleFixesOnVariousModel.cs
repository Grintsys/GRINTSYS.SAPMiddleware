using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GRINTSYS.SAPMiddleware.Migrations
{
    public partial class LittleFixesOnVariousModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastErrorMessage",
                table: "Payments",
                newName: "LastMessage");

            migrationBuilder.RenameColumn(
                name: "LastErrorMessage",
                table: "Orders",
                newName: "LastMessage");

            migrationBuilder.AddColumn<DateTime>(
                name: "PayedDate",
                table: "Payments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Banks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayedDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Banks");

            migrationBuilder.RenameColumn(
                name: "LastMessage",
                table: "Payments",
                newName: "LastErrorMessage");

            migrationBuilder.RenameColumn(
                name: "LastMessage",
                table: "Orders",
                newName: "LastErrorMessage");
        }
    }
}
