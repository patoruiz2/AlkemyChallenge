﻿// <auto-generated />
using System;
using AlkemyChallenge.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlkemyChallenge.Migrations
{
    [DbContext(typeof(DbContextModel))]
    partial class DbContextModelModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlkemyChallenge.Model.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("History")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Weigth")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.Character_Movie", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("MovieSerieId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId", "MovieSerieId");

                    b.HasIndex("MovieSerieId");

                    b.ToTable("Character_Movies");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.MovieSerie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Calification")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOrigin")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovieAndSeries");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.Movie_Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("MovieSerieId")
                        .HasColumnType("int");

                    b.HasKey("GenreId", "MovieSerieId");

                    b.HasIndex("MovieSerieId");

                    b.ToTable("Movie_Genre");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.RegisterUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("RegisterUsers");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.Character_Movie", b =>
                {
                    b.HasOne("AlkemyChallenge.Model.Character", "Character")
                        .WithMany("Character_Movies")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlkemyChallenge.Model.MovieSerie", "MovieSerie")
                        .WithMany("Character_Movies")
                        .HasForeignKey("MovieSerieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("MovieSerie");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.Movie_Genre", b =>
                {
                    b.HasOne("AlkemyChallenge.Model.Genre", "Genre")
                        .WithMany("Movie_Genres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlkemyChallenge.Model.MovieSerie", "MovieSerie")
                        .WithMany("Movie_Genres")
                        .HasForeignKey("MovieSerieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("MovieSerie");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.Character", b =>
                {
                    b.Navigation("Character_Movies");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.Genre", b =>
                {
                    b.Navigation("Movie_Genres");
                });

            modelBuilder.Entity("AlkemyChallenge.Model.MovieSerie", b =>
                {
                    b.Navigation("Character_Movies");

                    b.Navigation("Movie_Genres");
                });
#pragma warning restore 612, 618
        }
    }
}
