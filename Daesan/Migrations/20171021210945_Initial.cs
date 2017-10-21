using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Daesan.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Scene = table.Column<int>(type: "int", nullable: false),
                    Input = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResponseButtonLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseButtonUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsePhotoHeight = table.Column<int>(type: "int", nullable: false),
                    ResponsePhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsePhotoWidth = table.Column<int>(type: "int", nullable: false),
                    ResponseText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => new { x.Scene, x.Input });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentScene = table.Column<int>(type: "int", nullable: false),
                    FinishedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    StartedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
