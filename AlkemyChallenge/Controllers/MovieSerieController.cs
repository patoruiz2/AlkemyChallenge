using AlkemyChallenge.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieSerieController : ControllerBase
    {
        private readonly DbContextModel _context;
        public MovieSerieController(DbContextModel context)
        {
            _context = context;
        }
        //LIST
        [HttpGet]
        public IActionResult Get([FromQuery] string name, [FromQuery] int idGenre, [FromQuery] string orderFilter)
        {
            try
            {
                if (name != null && (idGenre == 0 && orderFilter == null))
                {
                    List<MovieSerie> nameCharac = (from c in _context.MovieAndSeries
                                                  where c.Title == name
                                                  select c).ToList();
                    return Ok(nameCharac);

                }
                else if (idGenre > 0 && (name == null && orderFilter == null))
                {
                    List<MovieSerie> ageCharac = (from c in _context.MovieAndSeries
                                                 where c.Id == idGenre
                                                 select c).ToList();
                    return Ok(ageCharac);
                }
                else if (orderFilter != null && (idGenre == 0 && name == null))
                {
                    switch (orderFilter)
                    {
                        case "asc":
                            var movieAsc = (from mo in _context.MovieAndSeries
                                            orderby mo.DateOrigin ascending
                                            select mo).ToList();
                            return Ok(movieAsc);
                        case "desc":
                            var movieDesc = (from mo in _context.MovieAndSeries
                                             orderby mo.DateOrigin descending
                                             select mo).ToList();
                            return Ok(movieDesc);
                    }
                }
                else if (name != null || idGenre != 0 || orderFilter != null)
                {
                    return NotFound("Ruta no existente");
                }
                var response = from c in _context.MovieAndSeries
                               select new { c.Id, c.Title, c.Picture };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        //CREATE
        [HttpPost("create")]
        public IActionResult PostMovie(MovieSerie model)
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
            _context.Add(movieSerie);
            _context.SaveChanges();
            return Ok(movieSerie);
        }
        //UPDATE
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] MovieSerie model)
        {
            var entityMS = _context.MovieAndSeries.Where(ms => ms.Id == id)
                .Include(cm => cm.Character_Movies)
                .ThenInclude(cm => cm.Character)
                .Include(mg => mg.Movie_Genres).ThenInclude(mg => mg.Genre)
                .FirstOrDefault();
            
            if (entityMS.Character_Movies.Any())
            {
                entityMS.Character_Movies.Clear();

                if (entityMS.Movie_Genres.Any())
                {
                    entityMS.Movie_Genres.Clear();
                }
            }
            entityMS.Title = model.Title;
            entityMS.Picture = model.Picture;
            entityMS.DateOrigin = model.DateOrigin;
            entityMS.Calification= model.Calification;
            entityMS.Character_Movies= model.Character_Movies;
            entityMS.Movie_Genres= model.Movie_Genres;


            _context.Entry(entityMS).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(entityMS);
        }
        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var entityMS = _context.MovieAndSeries.Where(ms => ms.Id == id)
                .FirstOrDefault();
            
            _context.Remove(entityMS);
            _context.SaveChanges();
            return Ok();
        }
        //DETAILS
        [HttpGet("{id}")]
        public IActionResult DetailsMovie(int id)
        {
            MovieSerie entityMS = _context.MovieAndSeries.Where(ms => ms.Id == id)
                .Include(ms => ms.Character_Movies).ThenInclude(cm => cm.Character)
                .Include(ms => ms.Movie_Genres).ThenInclude(gm => gm.Genre)
                .FirstOrDefault();
            
            if(entityMS == null)
            {
                return NotFound();
            }

            return Ok(entityMS);
        }
    }
}
