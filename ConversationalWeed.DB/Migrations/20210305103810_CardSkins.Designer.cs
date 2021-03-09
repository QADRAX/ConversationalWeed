﻿// <auto-generated />
using System;
using ConversationalWeed.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConversationalWeed.DB.Migrations
{
    [DbContext(typeof(WeedLeaderboardContext))]
    [Migration("20210305103810_CardSkins")]
    partial class CardSkins
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ConversationalWeed.DB.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("FinishedAtUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong?>("WinnerId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("WinnerId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("ConversationalWeed.DB.Models.Player", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("CurrentCardSkin")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("WeedCoins")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("ConversationalWeed.DB.Models.PlayerMatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<ulong>("PlayerId")
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("SmokedPoints")
                        .HasColumnType("int");

                    b.Property<int>("WeedPoints")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerMatch");
                });

            modelBuilder.Entity("ConversationalWeed.DB.Models.PlayerSkin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<ulong>("PlayerId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("SkinName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerSkin");
                });

            modelBuilder.Entity("ConversationalWeed.DB.Models.Match", b =>
                {
                    b.HasOne("ConversationalWeed.DB.Models.Player", "Winner")
                        .WithMany("WinMatches")
                        .HasForeignKey("WinnerId")
                        .HasConstraintName("FK_Match_WinnerPlayer");
                });

            modelBuilder.Entity("ConversationalWeed.DB.Models.PlayerMatch", b =>
                {
                    b.HasOne("ConversationalWeed.DB.Models.Match", "Match")
                        .WithMany("PlayerMatches")
                        .HasForeignKey("MatchId")
                        .HasConstraintName("FK_PlayerMatch_Match")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConversationalWeed.DB.Models.Player", "Player")
                        .WithMany("PlayerMatches")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("FK_PlayerMatch_Player")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConversationalWeed.DB.Models.PlayerSkin", b =>
                {
                    b.HasOne("ConversationalWeed.DB.Models.Player", "Player")
                        .WithMany("PlayerSkins")
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("FK_PlayerSkin_Player")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
