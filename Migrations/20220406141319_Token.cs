using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class Token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserID = table.Column<Guid>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    ExpireDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserID, x.Token });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTokens");
        }
    }
}
