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

        //LIST
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
        //CREATE
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

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteCharacter(int id)
        {
            var entityC = (from c in _context.Characters
                           where c.Id == id
                           select c).FirstOrDefault();
            _context.Remove(entityC);
            _context.SaveChanges();
            return Ok(entityC);
        }

        //UPDATE
        [HttpPut("{id}")]
        public IActionResult ModifyCharacter(int id,[FromBody] Character model)
        {
            var entityC = _context.Characters.Where(c => c.Id == id)
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


                _context.Entry(entityC).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(entityC);

            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }
        }
        //DETAILS
        [HttpGet("{id}")]
        public IActionResult DetailCharacter(int id)
        {
            var entityC = _context.Characters.Where(c => c.Id == id)
                .Include(cm => cm.Character_Movies).ThenInclude(cm => cm.MovieSerie)
                .FirstOrDefault();
            return Ok(entityC);
        }

    }
}

        //[HttpPost("send/charactermovie")]
        //public IActionResult PostCharacterMovie([FromQuery]int idCharacter, [FromQuery]int idMovieSerie)
        //{
        //    try
        //    {
        //        var character = _context.Characters.Where(c => c.Id == idCharacter).FirstOrDefault();
        //        var movieSerie = _context.MovieAndSeries.Where(ms => ms.Id == idMovieSerie).ToList();
        //        var characterMovie = new Character_Movie()
        //        {
        //            CharacterId = idCharacter,
        //            MovieSerieId = idMovieSerie,
        //        };

        //        _context.Add(characterMovie);
        //        _context.SaveChanges();

        //        return Ok(characterMovie);

        //    }catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

//        }