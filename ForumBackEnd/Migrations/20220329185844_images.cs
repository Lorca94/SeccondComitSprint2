using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumBackEnd.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Avatars_avatarId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Users_avatarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "avatarId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "avatarId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_avatarId",
                table: "Users",
                column: "avatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Avatars_avatarId",
                table: "Users",
                column: "avatarId",
                principalTable: "Avatars",
                principalColumn: "Id");
        }
    }
}
