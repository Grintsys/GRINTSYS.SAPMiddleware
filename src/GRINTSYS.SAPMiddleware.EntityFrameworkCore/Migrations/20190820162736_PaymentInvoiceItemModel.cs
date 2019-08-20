using Microsoft.EntityFrameworkCore.Migrations;

namespace GRINTSYS.SAPMiddleware.Migrations
{
    public partial class PaymentInvoiceItemModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInvoiceItem_Payments_PaymentId",
                table: "PaymentInvoiceItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentInvoiceItem",
                table: "PaymentInvoiceItem");

            migrationBuilder.RenameTable(
                name: "PaymentInvoiceItem",
                newName: "paymentInvoiceItems");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentInvoiceItem_PaymentId",
                table: "paymentInvoiceItems",
                newName: "IX_paymentInvoiceItems_PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paymentInvoiceItems",
                table: "paymentInvoiceItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_paymentInvoiceItems_Payments_PaymentId",
                table: "paymentInvoiceItems",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_paymentInvoiceItems_Payments_PaymentId",
                table: "paymentInvoiceItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paymentInvoiceItems",
                table: "paymentInvoiceItems");

            migrationBuilder.RenameTable(
                name: "paymentInvoiceItems",
                newName: "PaymentInvoiceItem");

            migrationBuilder.RenameIndex(
                name: "IX_paymentInvoiceItems_PaymentId",
                table: "PaymentInvoiceItem",
                newName: "IX_PaymentInvoiceItem_PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentInvoiceItem",
                table: "PaymentInvoiceItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInvoiceItem_Payments_PaymentId",
                table: "PaymentInvoiceItem",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
