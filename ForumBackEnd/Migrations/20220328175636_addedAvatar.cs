using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumBackEnd.Migrations
{
    public partial class addedAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
