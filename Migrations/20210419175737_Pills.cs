using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class Pills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pills",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FormFactor = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true),
                    DosageForm = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pills");
        }
    }
}
