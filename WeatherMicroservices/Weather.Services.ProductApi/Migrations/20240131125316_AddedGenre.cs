using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenreName",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenreId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "Name" },
                values: new object[] { new Guid("1de31dd1-5f13-4b54-a6e2-6ed196c1026f"), "Drinks" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1ae31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                columns: new[] { "Discount", "GenreId" },
                values: new object[] { null, new Guid("1de31dd1-5f13-4b54-a6e2-6ed196c1026f") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("2ae31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                columns: new[] { "Discount", "GenreId" },
                values: new object[] { null, new Guid("1de31dd1-5f13-4b54-a6e2-6ed196c1026f") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3ae31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                columns: new[] { "Discount", "GenreId" },
                values: new object[] { null, new Guid("1de31dd1-5f13-4b54-a6e2-6ed196c1026f") });

            migrationBuilder.CreateIndex(
                name: "IX_Products_GenreId",
                table: "Products",
                column: "GenreId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Discount_GreaterThanZero",
                table: "Products",
                sql: "Discount > 0");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Name",
                table: "Genre",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Genre_GenreId",
                table: "Products",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Genre_GenreId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Products_GenreId",
                table: "Products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Discount_GreaterThanZero",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "GenreName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1ae31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                column: "GenreName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("2ae31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                column: "GenreName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3ae31dd1-5f13-4b54-a6e2-6ed196c1026f"),
                column: "GenreName",
                value: null);
        }
    }
}
