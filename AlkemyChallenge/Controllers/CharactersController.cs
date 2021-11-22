using AlkemyChallenge.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {

        private readonly DbContextModel _context;


        public CharactersController(DbContextModel context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get([FromQuery] string name, [FromQuery] int? age = 0, [FromQuery] int movie = 0)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (name != null && (age == 0 && movie == 0))
                    {
                        List<Character> nameCharac = (from c in _context.Characters
                                                      where c.Name == name
                                                      select c).ToList();
                        return Ok(nameCharac);

                    } else if (age > 0 && (name == null && movie == 0))
                    {
                        List<Character> ageCharac = (from c in _context.Characters
                                                     where c.Age == age
                                                     select c).ToList();
                        return Ok(ageCharac);
                    }
                    else if (movie > 0 && (age == 0 && name == null))
                    {
                        var movieList = _context.MovieAndSeries.Where(m => m.Id == movie).Include(cm => cm.Character_Movies)
                        .ThenInclude(c => c.Character).ToList();

                        return Ok(movieList);
                    } else if (name != null || age != 0 || movie != 0)
                    {
                        return NotFound("Ruta no existente");
                    }
                    var response = from c in _context.Characters
                                   select new { c.Id, c.Name, c.Picture };

                    return Ok(response);

                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


        }

        [HttpPost("send/character")]
        public IActionResult PostCharacter(Character model)
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
            _context.Add(character);
            _context.SaveChanges();
            return Ok(character);
        }

        [HttpPost("send/movieserie")]
        public IActionResult PostMovie(MovieSerie movieSerie)
        {

            var send = new MovieSerie()
            {

                Picture = movieSerie.Picture,
                Title = movieSerie.Title,
                DateOrigin = movieSerie.DateOrigin,
                Calification = movieSerie.Calification,
                Character_Movies = movieSerie.Character_Movies,
                Movie_Genres = movieSerie.Movie_Genres
            };
            _context.AddRange(send);
            _context.SaveChanges();
            return Ok(send);
        }

        
    }
}
