using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify_API.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MusicPlayLists",
                table: "MusicPlayLists");

            migrationBuilder.DropIndex(
                name: "IX_MusicPlayLists_MusicId",
                table: "MusicPlayLists");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MusicPlayLists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusicPlayLists",
                table: "MusicPlayLists",
                columns: new[] { "MusicId", "PlayListId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MusicPlayLists",
                table: "MusicPlayLists");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MusicPlayLists",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusicPlayLists",
                table: "MusicPlayLists",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlayLists_MusicId",
                table: "MusicPlayLists",
                column: "MusicId");
        }
    }
}
