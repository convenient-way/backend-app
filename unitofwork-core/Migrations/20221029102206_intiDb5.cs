using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unitofwork_core.Migrations
{
    public partial class intiDb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Shop",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Shop",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Shipper",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Shipper",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Admin",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Admin",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_Email",
                table: "Shop",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shop_UserName",
                table: "Shop",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipper_Email",
                table: "Shipper",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipper_UserName",
                table: "Shipper",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Email",
                table: "Admin",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserName",
                table: "Admin",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shop_Email",
                table: "Shop");

            migrationBuilder.DropIndex(
                name: "IX_Shop_UserName",
                table: "Shop");

            migrationBuilder.DropIndex(
                name: "IX_Shipper_Email",
                table: "Shipper");

            migrationBuilder.DropIndex(
                name: "IX_Shipper_UserName",
                table: "Shipper");

            migrationBuilder.DropIndex(
                name: "IX_Admin_Email",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_UserName",
                table: "Admin");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Shop",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Shop",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Shipper",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Shipper",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
