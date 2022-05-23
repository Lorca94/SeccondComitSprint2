using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumBackEnd.Migrations
{
    public partial class noavatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
