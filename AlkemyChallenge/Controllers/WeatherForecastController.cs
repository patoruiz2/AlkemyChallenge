using AlkemyChallenge.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyChallenge.Controllers
{
    [ApiController]
    [Route("sources")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly DbContextModel _context;

        private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}
        public WeatherForecastController(DbContextModel context)
        {
            _context = context;
        }

        [HttpGet("characters")]
        public IQueryable Get()
        {
            var response = from c in _context.Characters
                           select new { c.Id,c.Name, c.Picture };

            return response;
        }

        [HttpGet("characters/{name}")]
        public IQueryable GetByName(string name)
        {
            var response = from c in _context.Characters
                           where c.Name == name
                           select new { c.Id, c.Name, c.Picture };

            return response;
        }
        [HttpGet("characters/{age}")]
        public IQueryable GetByAge(int age)
        {
            var response = from c in _context.Characters
                           where c.Age == age
                           select new { c.Id, c.Name, c.Picture };

            return response;
        }
        [HttpGet("characters/{movies}")]
        public IQueryable GetByIdMovie(int idMovie)
        {
            var response = from m in _context.MovieAndSeries
                           from c in _context.Characters
                           where m.Id == idMovie && c.
                           select new { c.Id, c.Name, c.Picture };

            return response;
        }
        [HttpPost("Send")]
        public Character Post(Character character)
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
    }
}
