using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class Rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorsRatings",
                columns: table => new
                {
                    DoctorID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsRatings", x => new { x.UserID, x.DoctorID });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorsRatings");
        }
    }
}
