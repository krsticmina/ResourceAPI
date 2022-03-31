using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceAPI.Migrations
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
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "ManagerId", "Name", "PhoneNumber", "Salary" },
                values: new object[] { 1, "jf@gmail.com", 0, "Jana Filipovic", "+38167543293", 1000 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "ManagerId", "Name", "PhoneNumber", "Salary" },
                values: new object[] { 2, "ev@gmail.com", 0, "Ema Vuckovic", "+38167235735", 500 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
