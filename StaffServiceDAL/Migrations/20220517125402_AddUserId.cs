using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffServiceDAL.Migrations
{
    public partial class AddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 5, 17, 12, 54, 1, 880, DateTimeKind.Utc).AddTicks(9922));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 5, 17, 12, 54, 1, 880, DateTimeKind.Utc).AddTicks(9928));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 5, 17, 12, 54, 1, 880, DateTimeKind.Utc).AddTicks(9929));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 4, 12, 9, 26, 8, 77, DateTimeKind.Utc).AddTicks(1434));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 4, 12, 9, 26, 8, 77, DateTimeKind.Utc).AddTicks(1439));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 4, 12, 9, 26, 8, 77, DateTimeKind.Utc).AddTicks(1440));
        }
    }
}
