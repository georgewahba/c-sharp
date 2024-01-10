﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asp_applicatie.Models;

#nullable disable

namespace asp_applicatie.Migrations
{
    [DbContext(typeof(AspApplicatieDbContext))]
    [Migration("20240110092627_tweede")]
    partial class tweede
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("asp_applicatie.Models.Album", b =>
                {
                    b.Property<int>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ArtistId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Album", (string)null);
                });

            modelBuilder.Entity("asp_applicatie.Models.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArtistName")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("ArtistId");

                    b.ToTable("Artist", (string)null);
                });

            modelBuilder.Entity("asp_applicatie.Models.Playlist", b =>
                {
                    b.Property<int>("PlaylistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlaylistId");

                    b.HasIndex("UserId");

                    b.ToTable("Playlist", (string)null);
                });

            modelBuilder.Entity("asp_applicatie.Models.PlaylistSong", b =>
                {
                    b.Property<int>("PlaylistSongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlaylistId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SongId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlaylistSongId");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("SongId");

                    b.ToTable("PlaylistSong", (string)null);
                });

            modelBuilder.Entity("asp_applicatie.Models.Song", b =>
                {
                    b.Property<int>("SongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AlbumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AudioFilePath")
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<int?>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Ratings")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("SongId");

                    b.HasIndex("AlbumId");

                    b.ToTable("Song", (string)null);
                });

            modelBuilder.Entity("asp_applicatie.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<int?>("LastPlayedSongId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("LastPlayedSongId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("asp_applicatie.Models.Album", b =>
                {
                    b.HasOne("asp_applicatie.Models.Artist", "artist")
                        .WithMany()
                        .HasForeignKey("ArtistId");

                    b.Navigation("artist");
                });

            modelBuilder.Entity("asp_applicatie.Models.Playlist", b =>
                {
                    b.HasOne("asp_applicatie.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("asp_applicatie.Models.PlaylistSong", b =>
                {
                    b.HasOne("asp_applicatie.Models.Playlist", "Playlist")
                        .WithMany("PlaylistSongs")
                        .HasForeignKey("PlaylistId");

                    b.HasOne("asp_applicatie.Models.Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId");

                    b.Navigation("Playlist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("asp_applicatie.Models.Song", b =>
                {
                    b.HasOne("asp_applicatie.Models.Album", "album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId");

                    b.Navigation("album");
                });

            modelBuilder.Entity("asp_applicatie.Models.User", b =>
                {
                    b.HasOne("asp_applicatie.Models.Song", "LastPlayedSong")
                        .WithMany()
                        .HasForeignKey("LastPlayedSongId");

                    b.Navigation("LastPlayedSong");
                });

            modelBuilder.Entity("asp_applicatie.Models.Album", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("asp_applicatie.Models.Playlist", b =>
                {
                    b.Navigation("PlaylistSongs");
                });
#pragma warning restore 612, 618
        }
    }
}
