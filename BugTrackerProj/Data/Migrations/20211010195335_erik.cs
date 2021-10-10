using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerProj.Data.Migrations
{
    public partial class erik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bugs",
                keyColumn: "BugId",
                keyValue: "o1",
                column: "BugDate",
                value: new DateTime(2021, 10, 10, 22, 53, 34, 808, DateTimeKind.Local).AddTicks(5462));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bugs",
                keyColumn: "BugId",
                keyValue: "o1",
                column: "BugDate",
                value: new DateTime(2021, 10, 9, 22, 30, 11, 487, DateTimeKind.Local).AddTicks(1651));
        }
    }
}
