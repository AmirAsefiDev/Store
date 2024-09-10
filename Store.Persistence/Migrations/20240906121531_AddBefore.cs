using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Persistence.Migrations
{
    public partial class AddBefore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserInRoles_Roles_RoleId",
            //    table: "UserInRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserInRoles_RoleId",
            //    table: "UserInRoles");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L);

            //migrationBuilder.DropColumn(
            //    name: "InsertTime",
            //    table: "Roles");


            //migrationBuilder.AddColumn<int>(
            //    name: "RoleId1",
            //    table: "UserInRoles",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Roles",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("SqlServer:Identity", "1, 1")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Operator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Customer" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserInRoles_RoleId1",
            //    table: "UserInRoles",
            //    column: "RoleId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserInRoles_Roles_RoleId1",
            //    table: "UserInRoles",
            //    column: "RoleId1",
            //    principalTable: "Roles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {         
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "InsertTime", "IsRemoved", "Name", "RemoveTime", "UpdateTime" },
                values: new object[] { 1L, new DateTime(2024, 9, 6, 15, 28, 50, 169, DateTimeKind.Local).AddTicks(6236), false, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "InsertTime", "IsRemoved", "Name", "RemoveTime", "UpdateTime" },
                values: new object[] { 2L, new DateTime(2024, 9, 6, 15, 28, 50, 170, DateTimeKind.Local).AddTicks(9885), false, "Operator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "InsertTime", "IsRemoved", "Name", "RemoveTime", "UpdateTime" },
                values: new object[] { 3L, new DateTime(2024, 9, 6, 15, 28, 50, 170, DateTimeKind.Local).AddTicks(9965), false, "Customer" });

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoles_RoleId",
                table: "UserInRoles",
                column: "RoleId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserInRoles_Roles_RoleId",
            //    table: "UserInRoles",
            //    column: "RoleId",
            //    principalTable: "Roles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserInRoles_Roles_RoleId1",
            //    table: "UserInRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserInRoles_RoleId1",
            //    table: "UserInRoles");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "UserInRoles");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Roles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");


        }
    }
}
