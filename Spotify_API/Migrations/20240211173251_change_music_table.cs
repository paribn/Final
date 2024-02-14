using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify_API.Migrations
{
    /// <inheritdoc />
    public partial class change_music_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListenCount",
                table: "Musics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListenCount",
                table: "Musics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
