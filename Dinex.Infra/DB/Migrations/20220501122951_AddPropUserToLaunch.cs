﻿//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dinex.Infra
{
    public partial class AddPropUserToLaunch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Launches",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Launches");
        }
    }
}
