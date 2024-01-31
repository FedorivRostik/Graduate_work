using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Services.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class addCustomerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7211", null, "CUSTOMER", "CUSTOMER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5de6906e-31a1-43d8-b8a8-ea2b3693f55d", "AQAAAAIAAYagAAAAEMqnpP3gI1VYP199vvNt1qMU/rAi8hzLUawSX4nCTuJBa8+ODDvtEqo5eW75YXD72w==", "580c5133-f026-4866-b4fd-40fee1844067" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7211");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0063dd52-e5a9-4bfc-b57d-06d0c4fd727a", "AQAAAAIAAYagAAAAELzyVahv32A3KUEa6rAB6V9k3/fPeLJ8ftNY5CZDg5IRwZ4pJYN1qb8V26mrnWJFnQ==", "8c6b7de3-a2a7-4df8-a864-2589823301ac" });
        }
    }
}
