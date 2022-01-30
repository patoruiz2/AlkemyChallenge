using AlkemyChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AlkemyChallenge.BL
{
    public class MovieSerieLogic
    {
        public Object Get( string name, int idGenre, string orderFilter, DbContextModel _context )
        {
            
            try
            {
                if ( name != null && ( idGenre == 0 && orderFilter == null ) )
                {
                    List<MovieSerie> nameCharac = (from c in _context.MovieAndSeries
                                                   where c.Title.Contains(name)
                                                   select c).ToList();
                    return nameCharac;

                }
                else if ( idGenre > 0 && ( name == null && orderFilter == null ) )
                {
                    List<MovieSerie> ageCharac = ( from c in _context.MovieAndSeries
                                                  where c.Id == idGenre
                                                  select c ).ToList();
                    return ageCharac;
                }
                else if (orderFilter != null && (idGenre == 0 && name == null))
                {
                    switch (orderFilter)
                    {
                        case "asc":
                            var movieAsc = ( from mo in _context.MovieAndSeries
                                            orderby mo.DateOrigin ascending
                                            select mo ).ToList();
                            return movieAsc;
                        case "desc":
                            var movieDesc = (from mo in _context.MovieAndSeries
                                             orderby mo.DateOrigin descending
                                             select mo).ToList();
                            return movieDesc;
                    }
                }
                else if ( name != null || idGenre != 0 || orderFilter != null )
                {
                    return "Ruta no existente";
                }
                var response = ( from c in _context.MovieAndSeries
                                select new { c.Id, c.Title, c.Picture } ).ToList();

                return response;

            }
            catch ( Exception ex )
            {
                return ex.Message;
            }
        }
        public Object Create( MovieSerie model )
        {
            var movieSerie = new MovieSerie()
            {
                Picture = model.Picture,
                Title = model.Title,
                DateOrigin = model.DateOrigin,
                Calification = model.Calification,
                Character_Movies = model.Character_Movies,
                Movie_Genres = model.Movie_Genres
            };

            return movieSerie;
        }

        public Object Update( int id, MovieSerie model, DbContextModel context)
        {
            var entityMovSer = context.MovieAndSeries.Where(ms => ms.Id == id)
               .Include(cm => cm.Character_Movies)
               .ThenInclude(cm => cm.Character)
               .Include(mg => mg.Movie_Genres).ThenInclude(mg => mg.Genre)
               .Include(mg => mg.Movie_Genres).ThenInclude(mg => mg.Genre)
               .FirstOrDefault();

            if (entityMovSer.Character_Movies.Any())
            {
                entityMovSer.Character_Movies.Clear();

                if (entityMovSer.Movie_Genres.Any())
                {
                    entityMovSer.Movie_Genres.Clear();
                }
            }
            entityMovSer.Title = model.Title;
            entityMovSer.Picture = model.Picture;
            entityMovSer.DateOrigin = model.DateOrigin;
            entityMovSer.Calification = model.Calification;
            entityMovSer.Character_Movies = model.Character_Movies;
            entityMovSer.Movie_Genres = model.Movie_Genres;

            return entityMovSer;
        }

        public Object Delete( int id, DbContextModel context )
        {
            var entityMS = context.MovieAndSeries.Where(ms => ms.Id == id)
                .FirstOrDefault();
            
            return entityMS;
        }

        public Object Details( int id, DbContextModel context)
        {
            MovieSerie entityMS = context.MovieAndSeries.Where(ms => ms.Id == id)
                .Include(ms => ms.Character_Movies).ThenInclude(cm => cm.Character)
                .Include(ms => ms.Movie_Genres).ThenInclude(gm => gm.Genre)
                .FirstOrDefault();
            
            return entityMS;
        }
    }
}