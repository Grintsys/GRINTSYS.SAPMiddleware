using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GRINTSYS.SAPMiddleware.Migrations
{
    public partial class RemoveDuplicateSizeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProductVariants_Size_SizeId",
                table: "CartProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Size_SizeId",
                table: "ProductVariants");

            migrationBuilder.DropTable(
                name: "M2Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Size",
                table: "Size");

            migrationBuilder.RenameTable(
                name: "Size",
                newName: "Sizes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProductVariants_Sizes_SizeId",
                table: "CartProductVariants",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Sizes_SizeId",
                table: "ProductVariants",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProductVariants_Sizes_SizeId",
                table: "CartProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Sizes_SizeId",
                table: "ProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes");

            migrationBuilder.RenameTable(
                name: "Sizes",
                newName: "Size");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Size",
                table: "Size",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "M2Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 65536, nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M2Sizes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CartProductVariants_Size_SizeId",
                table: "CartProductVariants",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Size_SizeId",
                table: "ProductVariants",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
