using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumBackEnd.Migrations
{
    public partial class answercreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSetted",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSetted",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Answers");
        }
    }
}
