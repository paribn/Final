using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify_API.Migrations
{
    /// <inheritdoc />
    public partial class update_grouptable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Groups");
        }
    }
}
