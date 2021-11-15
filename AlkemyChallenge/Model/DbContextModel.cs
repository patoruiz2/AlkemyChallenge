﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyChallenge.Model
{
    public class DbContextModel : DbContext
    {
        public DbContextModel(DbContextOptions<DbContextModel> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character_Movie>().HasKey(x => new { x.CharacterId, x.MovieSerieId });
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<MovieSerie> MovieAndSeries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Character_Movie> Character_Movies { get; set; }
        public DbSet<RegisterUser> RegisterUsers { get; set; }

    }
    public class Character
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25,ErrorMessage ="The field must be less than {0} characters")]
        [MinLength(3,ErrorMessage = "The field must be less than {0} characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength (3,ErrorMessage = "The field must be less than {0} characters")]
        public int Age { get; set; }
        public int Weigth { get; set; }
        public string History { get; set; }
        public byte[] Picture { get; set; }
        public List<Character_Movie> Character_Movies { get; set; }
    }

    public class MovieSerie
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "The field must be less than {0} characters")]
        public string Title { get; set; }
        [Required]
        public DateTime DateOrigin { get; set; }
        public int Calification { get; set; }
        public List<Character_Movie> Character_Movies { get; set; }
        public int GenreId { get; set; }

    }

    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage = "The field must be less than {0} characters")]
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public MovieSerie MovieSerie { get; set; }
    }

    public class Character_Movie
    {
        public int CharacterId { get; set; }
        public int MovieSerieId { get; set; }
        public Character Character { get; set; }
        public MovieSerie MovieSerie { get; set; }
    }

    public class RegisterUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

}
