using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify_API.Migrations
{
    /// <inheritdoc />
    public partial class create_new_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ArtistPhotos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ArtistPhotos");
        }
    }
}
