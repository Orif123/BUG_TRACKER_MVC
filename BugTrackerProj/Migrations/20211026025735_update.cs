using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerProj.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProjectId",
                table: "Categories",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Projects_ProjectId",
                table: "Categories",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Projects_ProjectId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProjectId",
                table: "Categories");
        }
    }
}
