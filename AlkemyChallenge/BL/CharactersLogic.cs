using AlkemyChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AlkemyChallenge.BL
{
    public class CharactersLogic
    {
        public Object Get( string name, int? age, int movie, DbContextModel _context )
        {
            try
            {
                if (name != null && (age == 0 && movie == 0))
                {
                    List<Character> nameCharac = (from c in _context.Characters
                                                    where c.Name.Contains(name)
                                                    select c).ToList();
                    return nameCharac;
                }
                else if (age > 0 && (name == null && movie == 0))
                {
                    List<Character> ageCharac = (from c in _context.Characters
                                                    where c.Age == age
                                                    select c).ToList();
                    return ageCharac;
                }
                else if (movie > 0 && (age == 0 && name == null))
                {
                    var movieList = _context.MovieAndSeries.Where(m => m.Id == movie).Include(cm => cm.Character_Movies)
                    .ThenInclude(c => c.Character).ToList();

                    return movieList;
                }
                else if (name != null || age != 0 || movie != 0)
                {
                    return "Ruta no existente";
                }
                var response = from c in _context.Characters
                                select new { c.Id, c.Name, c.Picture };
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public Object Create( Character model )
        {
            var character = new Character()
            {
                Age = model.Age,
                History = model.History,
                Name = model.Name,
                Picture = model.Picture,
                Weigth = model.Weigth,
                Character_Movies = model.Character_Movies
            };
            return character;
        }

        public Object Update( int id, Character model, DbContextModel context)
        {
            var entityC = context.Characters.Where(c => c.Id == id)
                .Include(cm => cm.Character_Movies).ThenInclude(cm => cm.MovieSerie)
                .FirstOrDefault();
            if (entityC.Character_Movies.Any())
            {
                entityC.Character_Movies.Clear();
            }
            try
            {
                entityC.Weigth = model.Weigth;
                entityC.Name = model.Name;
                entityC.Age = model.Age;
                entityC.Picture = model.Picture;
                entityC.History = model.History;
                entityC.Character_Movies = model.Character_Movies;


                context.Entry(entityC).State = EntityState.Modified;
                context.SaveChanges();
               return entityC;

            }
            catch (Exception ex)
            {
                return ex.InnerException;
            }
        }

        public Object Delete( int id, DbContextModel context )
        {
            var entityC = (from c in context.Characters
                           where c.Id == id
                           select c).FirstOrDefault();
            context.Remove(entityC);
            context.SaveChanges();
            return entityC;
        }

        public Object Details( int id, DbContextModel context)
        {
            var entityC = context.Characters.Where(c => c.Id == id)
                 .Include(cm => cm.Character_Movies).ThenInclude(cm => cm.MovieSerie)
                 .FirstOrDefault();
            
            return entityC;
        }
    }
}