using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyChallenge.Model
{
    public class DbContextModel : DbContext
    {
        public DbContextModel(DbContextOptions<DbContextModel> options) : base(options) 
        {

        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<MovieSerie> MovieAndSeries{ get; set; }
        public DbSet<Genre> Genres { get; set; }



    }
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weigth { get; set; }
        public string History { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<MovieSerie> MovieSeries { get; set; }
    }

    public class MovieSerie
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public string Title { get; set; }
        public DateTime DateOrigin { get; set; }
        public int Calification { get; set; }
        public ICollection<Character> Characters { get; set; }
        public ICollection<Genre> Genres{ get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<MovieSerie> MovieSerie { get; set; }
    }

}
