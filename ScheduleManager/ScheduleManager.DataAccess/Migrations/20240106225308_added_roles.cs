using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleManager.DataAccess.Migrations
{
    public partial class added_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e41581ea-fc9b-4317-8945-199c92473e7a"), "1", "SMA", "Schedule Manager Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e41581ea-fc9b-4317-8945-199c92473e7b"), "2", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e41581ea-fc9b-4317-8945-199c92473e7a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e41581ea-fc9b-4317-8945-199c92473e7b"));
        }
    }
}
