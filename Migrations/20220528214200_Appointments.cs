using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class Appointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Day = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Hour = table.Column<int>(nullable: false),
                    Minute = table.Column<int>(nullable: false),
                    DoctorID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => new { x.Hour, x.Minute, x.Day, x.Year, x.Month });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
