using Microsoft.EntityFrameworkCore.Migrations;

namespace GRINTSYS.SAPMiddleware.Migrations
{
    public partial class TenantModelFieldsIDontRecomendThisHere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "AbpTenants",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ISV",
                table: "AbpTenants",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "AbpTenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "AbpTenants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "ISV",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "AbpTenants");
        }
    }
}
