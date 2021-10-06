using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerProj.Data.Migrations
{
    public partial class ls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bugs",
                table: "Bugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "Bugs",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "1",
                column: "ProjectId",
                value: "1");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "2",
                column: "ProjectId",
                value: "1");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Categories", "CtaegoryName", "ProjectId" },
                values: new object[,]
                {
                    { "3", null, "Development", "2" },
                    { "4", null, "QA", "2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_Bugs",
                table: "Bugs",
                column: "Bugs");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bugs",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Projects_Bugs",
                table: "Bugs",
                column: "Bugs",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bugs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Projects_Bugs",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_Bugs",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bugs");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "4");

            migrationBuilder.DropColumn(
                name: "Bugs",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Bugs");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "1",
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "2",
                column: "ProjectId",
                value: null);
        }
    }
}
