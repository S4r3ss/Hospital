using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserRole",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    RoleOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName", "RoleOrder" },
                values: new object[,]
                {
                    { new Guid("1ab783cd-9eaf-4397-ad57-4d2a62abf48e"), "Client", 0 },
                    { new Guid("4c0f9bb5-fcc4-447d-baac-5d30363f757e"), "Doctor", 1 },
                    { new Guid("50c43a7e-76ab-4f66-9ff7-63cb8a3eb578"), "Head Doctor", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Birthday", "Email", "Gender", "Name", "Password", "Phone", "Surname", "UserRole" },
                values: new object[,]
                {
                    { new Guid("d269c295-8c9c-4e1d-97af-8f3bafaf6520"), "Chernivtsi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "Male", "Oleksandr", "P@ssw0rd", "380951483461", "Syrotiuk", new Guid("50c43a7e-76ab-4f66-9ff7-63cb8a3eb578") },
                    { new Guid("34ae203d-9185-482a-853a-19533e3ca44a"), "Chernivtsi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "client@gmail.com", "Male", "Pitsul", "P@ssw0rd", "380668122551", "Andrii", new Guid("4c0f9bb5-fcc4-447d-baac-5d30363f757e") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("34ae203d-9185-482a-853a-19533e3ca44a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d269c295-8c9c-4e1d-97af-8f3bafaf6520"));

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
