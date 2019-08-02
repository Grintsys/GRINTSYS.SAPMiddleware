using Microsoft.EntityFrameworkCore.Migrations;

namespace GRINTSYS.SAPMiddleware.Migrations
{
    public partial class PaymentModelsImprovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Cash_CashId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Transfers_TransferId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AbpUsers_UserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Transfers_TransferId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CashId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_TransferId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CashId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeviceUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TransferId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "TransferId",
                table: "Products",
                newName: "PaymentTransferId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TransferId",
                table: "Products",
                newName: "IX_Products_PaymentTransferId");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Transfers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Cash",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_PaymentId",
                table: "Transfers",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cash_PaymentId",
                table: "Cash",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cash_Payments_PaymentId",
                table: "Cash",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AbpUsers_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Transfers_PaymentTransferId",
                table: "Products",
                column: "PaymentTransferId",
                principalTable: "Transfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Payments_PaymentId",
                table: "Transfers",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cash_Payments_PaymentId",
                table: "Cash");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AbpUsers_UserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Transfers_PaymentTransferId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Payments_PaymentId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_PaymentId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Cash_PaymentId",
                table: "Cash");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Cash");

            migrationBuilder.RenameColumn(
                name: "PaymentTransferId",
                table: "Products",
                newName: "TransferId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_PaymentTransferId",
                table: "Products",
                newName: "IX_Products_TransferId");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "CashId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeviceUserId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransferId",
                table: "Payments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CashId",
                table: "Payments",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransferId",
                table: "Payments",
                column: "TransferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Cash_CashId",
                table: "Payments",
                column: "CashId",
                principalTable: "Cash",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Transfers_TransferId",
                table: "Payments",
                column: "TransferId",
                principalTable: "Transfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AbpUsers_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Transfers_TransferId",
                table: "Products",
                column: "TransferId",
                principalTable: "Transfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
