using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dinex.WebApi.Infra.DB.Migrations
{
    public partial class AddLaunchAndPayMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryiesToUsers",
                table: "CategoryiesToUsers");

            migrationBuilder.RenameTable(
                name: "CategoryiesToUsers",
                newName: "CategoriesToUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriesToUsers",
                table: "CategoriesToUsers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Launches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Launches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayMethodFromLaunches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PayMethod = table.Column<int>(type: "INTEGER", nullable: false),
                    LaunchId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayMethodFromLaunches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Launches");

            migrationBuilder.DropTable(
                name: "PayMethodFromLaunches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriesToUsers",
                table: "CategoriesToUsers");

            migrationBuilder.RenameTable(
                name: "CategoriesToUsers",
                newName: "CategoryiesToUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryiesToUsers",
                table: "CategoryiesToUsers",
                column: "Id");
        }
    }
}
