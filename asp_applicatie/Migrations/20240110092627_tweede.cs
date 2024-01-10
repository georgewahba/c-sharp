using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_applicatie.Migrations
{
    /// <inheritdoc />
    public partial class tweede : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArtistName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "TEXT", unicode: false, maxLength: 500, nullable: true),
                    Genre = table.Column<string>(type: "TEXT", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", unicode: false, maxLength: 500, nullable: true),
                    ArtistId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.AlbumId);
                    table.ForeignKey(
                        name: "FK_Album_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId");
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", unicode: false, maxLength: 200, nullable: true),
                    AudioFilePath = table.Column<string>(type: "TEXT", unicode: false, nullable: true),
                    AlbumId = table.Column<int>(type: "INTEGER", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true),
                    Ratings = table.Column<int>(type: "INTEGER", unicode: false, maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.SongId);
                    table.ForeignKey(
                        name: "FK_Song_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "AlbumId");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", unicode: false, maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "TEXT", unicode: false, maxLength: 200, nullable: true),
                    FullName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 500, nullable: true),
                    LastPlayedSongId = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Song_LastPlayedSongId",
                        column: x => x.LastPlayedSongId,
                        principalTable: "Song",
                        principalColumn: "SongId");
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    PlaylistId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", unicode: false, maxLength: 500, nullable: true),
                    IsPrivate = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.PlaylistId);
                    table.ForeignKey(
                        name: "FK_Playlist_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PlaylistSong",
                columns: table => new
                {
                    PlaylistSongId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaylistId = table.Column<int>(type: "INTEGER", nullable: true),
                    SongId = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistSong", x => x.PlaylistSongId);
                    table.ForeignKey(
                        name: "FK_PlaylistSong_Playlist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlist",
                        principalColumn: "PlaylistId");
                    table.ForeignKey(
                        name: "FK_PlaylistSong_Song_SongId",
                        column: x => x.SongId,
                        principalTable: "Song",
                        principalColumn: "SongId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistId",
                table: "Album",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_UserId",
                table: "Playlist",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSong_PlaylistId",
                table: "PlaylistSong",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSong_SongId",
                table: "PlaylistSong",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_AlbumId",
                table: "Song",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LastPlayedSongId",
                table: "User",
                column: "LastPlayedSongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistSong");

            migrationBuilder.DropTable(
                name: "Playlist");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Artist");
        }
    }
}
