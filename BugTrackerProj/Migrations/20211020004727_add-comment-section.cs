using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerProj.Migrations
{
    public partial class addcommentsection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    BugId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Bugs_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Bugs",
                        principalColumn: "BugId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
