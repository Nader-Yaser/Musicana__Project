using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Musicana.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaylists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoverImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlist_Songs",
                columns: table => new
                {
                    PlaylistId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist_Songs", x => new { x.PlaylistId, x.SongId });
                    table.ForeignKey(
                        name: "FK_Playlist_Songs_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Playlist_Songs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "CoverImagePath", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "/CoverImages/playlist1.jpg", "Relaxing songs for chill time", false, "Chill Vibes" },
                    { 2, "/CoverImages/playlist2.jpg", "High energy songs for workout", false, "Workout Mix" },
                    { 3, "/CoverImages/playlist3.jpg", "Best songs for driving", false, "Road Trip" }
                });

            migrationBuilder.InsertData(
                table: "Playlist_Songs",
                columns: new[] { "PlaylistId", "SongId", "Order" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 1, 3, 2 },
                    { 1, 5, 3 },
                    { 2, 2, 1 },
                    { 2, 4, 2 },
                    { 2, 6, 3 },
                    { 3, 1, 1 },
                    { 3, 7, 2 },
                    { 3, 8, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_Songs_SongId",
                table: "Playlist_Songs",
                column: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Playlist_Songs");

            migrationBuilder.DropTable(
                name: "Playlists");
        }
    }
}
