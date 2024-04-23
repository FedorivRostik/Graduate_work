using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Services.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class ExtendUserWithNewProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Age",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvgDonwDialysticPressure",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvgDownSystolicPressure",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvgUpDialysticPressure",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvgUpSystolicPressure",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Pressure",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "Age", "AvgDonwDialysticPressure", "AvgDownSystolicPressure", "AvgUpDialysticPressure", "AvgUpSystolicPressure", "City", "ConcurrencyStamp", "PasswordHash", "Pressure", "Region", "SecurityStamp", "Weight" },
                values: new object[] { 0.0, 0.0, 0.0, 0.0, 0.0, "", "292986f0-18f8-401c-b7b0-a86bbe573e80", "AQAAAAIAAYagAAAAEG2x1YFDASkvHMWsiBh5z0yuuPECiyki4AeE7v1xnuwosUyizL1vDNIhKlLSxLSW2w==", 0, "", "935a3deb-9405-4369-96b5-ce6adaec9a5a", 0.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvgDonwDialysticPressure",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvgDownSystolicPressure",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvgUpDialysticPressure",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvgUpSystolicPressure",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5de6906e-31a1-43d8-b8a8-ea2b3693f55d", "AQAAAAIAAYagAAAAEMqnpP3gI1VYP199vvNt1qMU/rAi8hzLUawSX4nCTuJBa8+ODDvtEqo5eW75YXD72w==", "580c5133-f026-4866-b4fd-40fee1844067" });
        }
    }
}
