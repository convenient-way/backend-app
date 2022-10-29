using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unitofwork_core.Migrations
{
    public partial class initDb6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Shipper_ShipperId",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShipperId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Shipper_ShipperId",
                table: "Order",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Shipper_ShipperId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShipperId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Shipper_ShipperId",
                table: "Order",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
