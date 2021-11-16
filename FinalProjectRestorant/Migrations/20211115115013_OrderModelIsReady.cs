using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectRestorant.Migrations
{
    public partial class OrderModelIsReady : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "SteakBigMenus",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "starters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Pizzas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "drinks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "desserts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "breakFasts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SteakBigId = table.Column<int>(nullable: false),
                    PreakFastsId = table.Column<int>(nullable: false),
                    PizzaId = table.Column<int>(nullable: false),
                    StartersId = table.Column<int>(nullable: false),
                    DessertId = table.Column<int>(nullable: false),
                    DrinksId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SteakBigMenus_OrderId",
                table: "SteakBigMenus",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_starters_OrderId",
                table: "starters",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_OrderId",
                table: "Pizzas",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_drinks_OrderId",
                table: "drinks",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_desserts_OrderId",
                table: "desserts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_breakFasts_OrderId",
                table: "breakFasts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_breakFasts_Orders_OrderId",
                table: "breakFasts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_desserts_Orders_OrderId",
                table: "desserts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_drinks_Orders_OrderId",
                table: "drinks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Orders_OrderId",
                table: "Pizzas",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_starters_Orders_OrderId",
                table: "starters",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SteakBigMenus_Orders_OrderId",
                table: "SteakBigMenus",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_breakFasts_Orders_OrderId",
                table: "breakFasts");

            migrationBuilder.DropForeignKey(
                name: "FK_desserts_Orders_OrderId",
                table: "desserts");

            migrationBuilder.DropForeignKey(
                name: "FK_drinks_Orders_OrderId",
                table: "drinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Orders_OrderId",
                table: "Pizzas");

            migrationBuilder.DropForeignKey(
                name: "FK_starters_Orders_OrderId",
                table: "starters");

            migrationBuilder.DropForeignKey(
                name: "FK_SteakBigMenus_Orders_OrderId",
                table: "SteakBigMenus");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_SteakBigMenus_OrderId",
                table: "SteakBigMenus");

            migrationBuilder.DropIndex(
                name: "IX_starters_OrderId",
                table: "starters");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_OrderId",
                table: "Pizzas");

            migrationBuilder.DropIndex(
                name: "IX_drinks_OrderId",
                table: "drinks");

            migrationBuilder.DropIndex(
                name: "IX_desserts_OrderId",
                table: "desserts");

            migrationBuilder.DropIndex(
                name: "IX_breakFasts_OrderId",
                table: "breakFasts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "SteakBigMenus");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "starters");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "drinks");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "desserts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "breakFasts");
        }
    }
}
