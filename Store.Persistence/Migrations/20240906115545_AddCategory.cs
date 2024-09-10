using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Persistence.Migrations
{
    public partial class AddCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 9, 6, 15, 25, 45, 317, DateTimeKind.Local).AddTicks(5087));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 9, 6, 15, 25, 45, 318, DateTimeKind.Local).AddTicks(8158));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 9, 6, 15, 25, 45, 318, DateTimeKind.Local).AddTicks(8235));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 9, 6, 15, 20, 35, 740, DateTimeKind.Local).AddTicks(3554));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 9, 6, 15, 20, 35, 741, DateTimeKind.Local).AddTicks(7624));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 9, 6, 15, 20, 35, 741, DateTimeKind.Local).AddTicks(7703));
        }
    }
}
