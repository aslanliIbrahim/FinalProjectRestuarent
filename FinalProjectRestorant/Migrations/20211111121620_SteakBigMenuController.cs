using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectRestorant.Migrations
{
    public partial class SteakBigMenuController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SteakBigMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: true),
                    BigMenuFoodName = table.Column<string>(maxLength: 100, nullable: false),
                    Ingredient = table.Column<string>(maxLength: 100, nullable: false),
                    Servis = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Pieces = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteakBigMenus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SteakBigMenus");
        }
    }
}
