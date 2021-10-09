using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerProj.Data.Migrations
{
    public partial class itzik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bugs",
                keyColumn: "BugId",
                keyValue: "o1",
                column: "BugDate",
                value: new DateTime(2021, 10, 9, 22, 30, 11, 487, DateTimeKind.Local).AddTicks(1651));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bugs",
                keyColumn: "BugId",
                keyValue: "o1",
                column: "BugDate",
                value: new DateTime(2021, 10, 9, 22, 18, 29, 710, DateTimeKind.Local).AddTicks(382));
        }
    }
}
