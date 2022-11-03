using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unitofwork_core.Migrations
{
    public partial class initdb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryOrder_Package_PackageId",
                table: "HistoryOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryOrder",
                table: "HistoryOrder");

            migrationBuilder.RenameTable(
                name: "HistoryOrder",
                newName: "HistoryPackage");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryOrder_PackageId",
                table: "HistoryPackage",
                newName: "IX_HistoryPackage_PackageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryPackage",
                table: "HistoryPackage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryPackage_Package_PackageId",
                table: "HistoryPackage",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryPackage_Package_PackageId",
                table: "HistoryPackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryPackage",
                table: "HistoryPackage");

            migrationBuilder.RenameTable(
                name: "HistoryPackage",
                newName: "HistoryOrder");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryPackage_PackageId",
                table: "HistoryOrder",
                newName: "IX_HistoryOrder_PackageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryOrder",
                table: "HistoryOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryOrder_Package_PackageId",
                table: "HistoryOrder",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
