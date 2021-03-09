using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConversationalWeed.DB.Migrations
{
    public partial class CardSkins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentCardSkin",
                table: "Player",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayerSkin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<ulong>(nullable: false),
                    SkinName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSkin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerSkin_Player",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSkin_PlayerId",
                table: "PlayerSkin",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerSkin");

            migrationBuilder.DropColumn(
                name: "CurrentCardSkin",
                table: "Player");
        }
    }
}
