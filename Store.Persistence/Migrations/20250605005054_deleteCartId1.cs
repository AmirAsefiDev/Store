using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Persistence.Migrations
{
    public partial class deleteCartId1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CartItems_Carts_CartId1",
            //    table: "CartItems");

            //migrationBuilder.DropIndex(
            //    name: "IX_CartItems_CartId1",
            //    table: "CartItems");

            //migrationBuilder.DropColumn(
            //    name: "CartId1",
            //    table: "CartItems");

            migrationBuilder.AlterColumn<long>(
                name: "CartId",
                table: "CartItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 6, 5, 4, 20, 53, 819, DateTimeKind.Local).AddTicks(9761));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 6, 5, 4, 20, 53, 821, DateTimeKind.Local).AddTicks(4580));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 6, 5, 4, 20, 53, 821, DateTimeKind.Local).AddTicks(4654));

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CartId1",
                table: "CartItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 5, 30, 7, 26, 38, 582, DateTimeKind.Local).AddTicks(5405));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 5, 30, 7, 26, 38, 584, DateTimeKind.Local).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 5, 30, 7, 26, 38, 584, DateTimeKind.Local).AddTicks(8498));

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId1",
                table: "CartItems",
                column: "CartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId1",
                table: "CartItems",
                column: "CartId1",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
