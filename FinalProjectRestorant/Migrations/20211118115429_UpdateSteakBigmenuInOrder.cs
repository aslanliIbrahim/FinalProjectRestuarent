using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectRestorant.Migrations
{
    public partial class UpdateSteakBigmenuInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SteakBigMenus_Orders_OrderId",
                table: "SteakBigMenus");

            migrationBuilder.DropIndex(
                name: "IX_SteakBigMenus_OrderId",
                table: "SteakBigMenus");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "SteakBigMenus");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SteakBigId",
                table: "Orders",
                column: "SteakBigId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SteakBigMenus_SteakBigId",
                table: "Orders",
                column: "SteakBigId",
                principalTable: "SteakBigMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SteakBigMenus_SteakBigId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SteakBigId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "SteakBigMenus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SteakBigMenus_OrderId",
                table: "SteakBigMenus",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SteakBigMenus_Orders_OrderId",
                table: "SteakBigMenus",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
