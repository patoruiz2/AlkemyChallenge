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
    [ApiController]
    [Route("/characters")]
    public class CharactersController : ControllerBase
    {

        private readonly DbContextModel _context;

        
        public CharactersController(DbContextModel context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/{name}/{age:int?}/{movie:int?}")]
        public ActionResult Get([FromQuery] string name,[FromQuery] int? age = 0,[FromQuery] int movie = 0)
        {
            try
            {
               if(name != null && (age == 0 && movie == 0))
                {
                    List<Character> nameCharac = (from c in _context.Characters
                                                     where c.Name == name
                                                     select c).ToList();
                    return Ok(nameCharac);

                }else if(age > 0 && (name == null && movie == 0))
                {
                    List<Character> ageCharac = (from c in _context.Characters
                                                where c.Age == age
                                                select c).ToList();
                    return Ok(ageCharac);
                }
                else if(movie > 0 && (age==0 && name == null))
                {
                    var movieList = _context.MovieAndSeries.Where(m => m.Id == movie).Include(cm => cm.Character_Movies)
                    .ThenInclude(c => c.Character).ToList();

                    return Ok(movieList);
                }else if(name != null || age != 0 || movie != 0)
                {
                    return NotFound("Ruta no existente");
                }
                var response = from c in _context.Characters
                               select new { c.Id,c.Name, c.Picture };
            
                return Ok(response);

            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }


        }

        [HttpPost("send/character")]
        public Character PostCharacter(Character character)
        {
            
            var send = new Character()
            {
                
                Age = character.Age,
                History = character.History,
                Name = character.Name,
                Picture = character.Picture,
                Weigth = character.Weigth,

            };
            _context.Add(send);
            _context.SaveChanges();
            return send;
        }

        [HttpPost("send/movieserie")]
        public MovieSerie PostMovie(MovieSerie movieSerie)
        {
            var send = new MovieSerie()
            {
                
               Picture= movieSerie.Picture,
               Title = movieSerie.Title,
               DateOrigin = movieSerie.DateOrigin,
               Calification = movieSerie.Calification,
               

            };
            _context.Add(send);
            _context.SaveChanges();
            return send;
        }
        [HttpPost("send/charactermovie")]
        public Character_Movie PostCharacterMovie(MovieSerie movieSerie)
        {
            //var character = _context.Characters.Where(c => c.Id == characters.Id).FirstOrDefault();
            var movieSeries = _context.MovieAndSeries.Where(ms => ms.Id == movieSerie.Id).ToList();
            var send = new Character_Movie()
            {
                //CharacterId = character.Id,
                MovieSerieId = movieSerie.Id,
            };
            _context.Add(send);
            _context.SaveChanges();
            return send;
        }
    }
}
