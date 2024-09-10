using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Persistence.Migrations
{
    public partial class The_Latest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserInRoles_Roles_RoleId1",
            //    table: "UserInRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserInRoles_RoleId1",
            //    table: "UserInRoles");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: 2);

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: 3);

            //migrationBuilder.DropColumn(
            //    name: "RoleId1",
            //    table: "UserInRoles");

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Roles",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

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

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserInRoles_RoleId",
            //    table: "UserInRoles",
            //    column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserInRoles_Roles_RoleId",
            //    table: "UserInRoles",
            //    column: "RoleId",
            //    principalTable: "Roles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserInRoles_Roles_RoleId",
            //    table: "UserInRoles");

            //migrationBuilder.DropTable(
            //    name: "Categories");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserInRoles_RoleId",
            //    table: "UserInRoles");

            //migrationBuilder.DropColumn(
            //    name: "InsertTime",
            //    table: "Roles");

            //migrationBuilder.DropColumn(
            //    name: "IsRemoved",
            //    table: "Roles");

            //migrationBuilder.DropColumn(
            //    name: "RemoveTime",
            //    table: "Roles");

            //migrationBuilder.DropColumn(
            //    name: "UpdateTime",
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

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoles_RoleId1",
                table: "UserInRoles",
                column: "RoleId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserInRoles_Roles_RoleId1",
            //    table: "UserInRoles",
            //    column: "RoleId1",
            //    principalTable: "Roles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
