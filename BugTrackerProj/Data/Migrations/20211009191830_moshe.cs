using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerProj.Data.Migrations
{
    public partial class moshe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bugs",
                keyColumn: "BugId",
                keyValue: "o1",
                columns: new[] { "BugDate", "CategoryId", "ProjectId", "UserId" },
                values: new object[] { new DateTime(2021, 10, 9, 22, 18, 29, 710, DateTimeKind.Local).AddTicks(382), "1", "1", "7f3c9ad6-090f-444a-8dd3-9e4179dcbac7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bugs",
                keyColumn: "BugId",
                keyValue: "o1",
                columns: new[] { "BugDate", "CategoryId", "ProjectId", "UserId" },
                values: new object[] { new DateTime(2021, 10, 6, 22, 3, 4, 301, DateTimeKind.Local).AddTicks(3300), null, null, null });
        }
    }
}
