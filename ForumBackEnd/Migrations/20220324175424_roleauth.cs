using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumBackEnd.Migrations
{
    public partial class roleauth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Modules_ModuleId",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Modules_ModuleId",
                table: "Questions",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Modules_ModuleId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Modules_ModuleId",
                table: "Questions",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
