using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumBackEnd.Migrations
{
    public partial class AddSubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suscriber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscriber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suscriber_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suscriber_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suscriber_QuestionId",
                table: "Suscriber",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Suscriber_UserId",
                table: "Suscriber",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suscriber");
        }
    }
}
