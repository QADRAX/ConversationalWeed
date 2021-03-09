using Microsoft.EntityFrameworkCore.Migrations;

namespace ConversationalWeed.DB.Migrations
{
    public partial class WeedCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "WeedCoins",
                table: "Player",
                nullable: false,
                defaultValue: 0ul);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeedCoins",
                table: "Player");
        }
    }
}
