using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectRestorant.Migrations
{
    public partial class CreateOpenTimesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenTimes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hours = table.Column<string>(nullable: false),
                    MealTime = table.Column<string>(nullable: false),
                    Day = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenTimes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenTimes");
        }
    }
}
