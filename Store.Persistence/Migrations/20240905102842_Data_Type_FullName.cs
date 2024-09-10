using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Persistence.Migrations
{
    public partial class Data_Type_FullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInRoles_Users_UserId1",
                table: "UserInRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserInRoles_UserId1",
                table: "UserInRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserInRoles");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoles_UserId",
                table: "UserInRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInRoles_Users_UserId",
                table: "UserInRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInRoles_Users_UserId",
                table: "UserInRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserInRoles_UserId",
                table: "UserInRoles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserInRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoles_UserId1",
                table: "UserInRoles",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInRoles_Users_UserId1",
                table: "UserInRoles",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
