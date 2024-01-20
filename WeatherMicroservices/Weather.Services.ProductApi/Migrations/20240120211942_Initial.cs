using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Weather.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.CheckConstraint("CK_Amount_GreaterThanZero", "Amount > 0");
                    table.CheckConstraint("CK_Price_GreaterThanZero", "Price > 0");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "CouponCode", "Description", "GenreName", "ImageUrl", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { new Guid("1ae31dd1-5f13-4b54-a6e2-6ed196c1026f"), 100, null, null, null, null, "Cola", 0.89m, "cola" },
                    { new Guid("2ae31dd1-5f13-4b54-a6e2-6ed196c1026f"), 110, null, null, null, null, "Pepsi", 0.49m, "pepsi" },
                    { new Guid("3ae31dd1-5f13-4b54-a6e2-6ed196c1026f"), 120, null, null, null, null, "Fanta", 0.59m, "fanta" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Slug",
                table: "Products",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
