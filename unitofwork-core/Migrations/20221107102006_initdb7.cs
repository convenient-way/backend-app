using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unitofwork_core.Migrations
{
    public partial class initdb7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationLatitude",
                table: "Shipper");

            migrationBuilder.DropColumn(
                name: "DestinationLongitude",
                table: "Shipper");

            migrationBuilder.DropColumn(
                name: "HomeLatitude",
                table: "Shipper");

            migrationBuilder.DropColumn(
                name: "HomeLongitude",
                table: "Shipper");

            migrationBuilder.CreateTable(
                name: "ShipperRoute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromLongitude = table.Column<double>(type: "float", nullable: false),
                    FromLatitude = table.Column<double>(type: "float", nullable: false),
                    ToAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToLongitude = table.Column<double>(type: "float", nullable: false),
                    ToLatitude = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ShipperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperRoute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipperRoute_Shipper_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shipper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipperRoute_ShipperId",
                table: "ShipperRoute",
                column: "ShipperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipperRoute");

            migrationBuilder.AddColumn<double>(
                name: "DestinationLatitude",
                table: "Shipper",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DestinationLongitude",
                table: "Shipper",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HomeLatitude",
                table: "Shipper",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HomeLongitude",
                table: "Shipper",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
