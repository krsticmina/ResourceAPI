using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffServiceDAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "Email", "ManagerId", "ModifiedAt", "Name", "Position" },
                values: new object[] { 1, new DateTime(2022, 4, 12, 9, 26, 8, 77, DateTimeKind.Utc).AddTicks(1434), "jf@gmail.com", null, null, "Jana Filipovic", "Admin" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "Email", "ManagerId", "ModifiedAt", "Name", "Position" },
                values: new object[] { 2, new DateTime(2022, 4, 12, 9, 26, 8, 77, DateTimeKind.Utc).AddTicks(1439), "ev@gmail.com", null, null, "Ema Vuckovic", "Manager" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "Email", "ManagerId", "ModifiedAt", "Name", "Position" },
                values: new object[] { 3, new DateTime(2022, 4, 12, 9, 26, 8, 77, DateTimeKind.Utc).AddTicks(1440), "acim@gmail.com", 2, null, "Adam Cimbaljevic", "Employee" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
