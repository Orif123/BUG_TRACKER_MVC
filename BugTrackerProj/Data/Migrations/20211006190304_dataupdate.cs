using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerProj.Data.Migrations
{
    public partial class dataupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_AspNetUsers_BugId",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bugs");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Bugs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Bugs",
                columns: new[] { "BugId", "BugDate", "Bugs", "CategoryId", "Description", "ProjectId", "UserId" },
                values: new object[] { "o1", new DateTime(2021, 10, 6, 22, 3, 4, 301, DateTimeKind.Local).AddTicks(3300), null, null, "ori", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bugs",
                keyColumn: "BugId",
                keyValue: "o1");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Bugs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bugs",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_AspNetUsers_BugId",
                table: "Bugs",
                column: "BugId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bugs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
