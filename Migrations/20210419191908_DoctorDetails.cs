using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class DoctorDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Doctors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Doctors");
        }
    }
}
