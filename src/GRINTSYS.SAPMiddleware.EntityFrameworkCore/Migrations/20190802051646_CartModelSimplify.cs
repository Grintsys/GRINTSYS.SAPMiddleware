using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GRINTSYS.SAPMiddleware.Migrations
{
    public partial class CartModelSimplify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProductItems_CartProductVariants_CartProductVariantId",
                table: "CartProductItems");

            migrationBuilder.DropTable(
                name: "CartProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_CartProductItems_CartProductVariantId",
                table: "CartProductItems");

            migrationBuilder.DropColumn(
                name: "CartProductVariantId",
                table: "CartProductItems");

            migrationBuilder.AddColumn<int>(
                name: "ProductVariantId",
                table: "CartProductItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartProductItems_ProductVariantId",
                table: "CartProductItems",
                column: "ProductVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProductItems_ProductVariants_ProductVariantId",
                table: "CartProductItems",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProductItems_ProductVariants_ProductVariantId",
                table: "CartProductItems");

            migrationBuilder.DropIndex(
                name: "IX_CartProductItems_ProductVariantId",
                table: "CartProductItems");

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "CartProductItems");

            migrationBuilder.AddColumn<int>(
                name: "CartProductVariantId",
                table: "CartProductItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CartProductVariants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    DiscountPercent = table.Column<double>(nullable: false),
                    MainImage = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PriceFormatted = table.Column<string>(nullable: true),
                    ProductVariantId = table.Column<int>(nullable: true),
                    SizeId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    WareHouseCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartProductVariants_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProductVariants_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProductVariants_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartProductVariants_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProductItems_CartProductVariantId",
                table: "CartProductItems",
                column: "CartProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductVariants_CategoryId",
                table: "CartProductVariants",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductVariants_ColorId",
                table: "CartProductVariants",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductVariants_ProductVariantId",
                table: "CartProductVariants",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductVariants_SizeId",
                table: "CartProductVariants",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProductItems_CartProductVariants_CartProductVariantId",
                table: "CartProductItems",
                column: "CartProductVariantId",
                principalTable: "CartProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
