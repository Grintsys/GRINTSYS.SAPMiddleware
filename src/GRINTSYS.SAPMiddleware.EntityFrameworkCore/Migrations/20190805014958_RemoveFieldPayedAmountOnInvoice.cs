using Microsoft.EntityFrameworkCore.Migrations;

namespace GRINTSYS.SAPMiddleware.Migrations
{
    public partial class RemoveFieldPayedAmountOnInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayedAmount",
                table: "Invoices");

            migrationBuilder.AlterColumn<double>(
                name: "OverdueDays",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OverdueDays",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<double>(
                name: "PayedAmount",
                table: "Invoices",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
