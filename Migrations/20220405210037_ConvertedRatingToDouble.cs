using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class ConvertedRatingToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "DoctorsRatings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "DoctorsRatings",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
