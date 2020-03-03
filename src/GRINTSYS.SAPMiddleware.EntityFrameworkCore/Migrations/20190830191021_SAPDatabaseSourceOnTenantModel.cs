using Microsoft.EntityFrameworkCore.Migrations;

namespace GRINTSYS.SAPMiddleware.Migrations
{
    public partial class SAPDatabaseSourceOnTenantModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SAPDatabase",
                table: "AbpTenants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SAPDatabase",
                table: "AbpTenants");
        }
    }
}
