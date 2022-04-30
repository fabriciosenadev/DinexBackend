using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dinex.WebApi.Infra.DB.Migrations
{
    public partial class MigrateApplicable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Applicable",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "Applicable",
                table: "CategoriesToUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Applicable",
                table: "CategoriesToUsers");

            migrationBuilder.AddColumn<int>(
                name: "Applicable",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
