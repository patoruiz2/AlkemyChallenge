using AlkemyChallenge.Model;
using AlkemyChallenge.BL;
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
        private readonly MovieSerieLogic _bussinessLogic = new MovieSerieLogic();
        public MovieSerieController(DbContextModel context)
        {
            _context = context;
        }
        //LIST
        [HttpGet]
        public IActionResult Get([FromQuery] string name, [FromQuery] int idGenre, [FromQuery] string orderFilter)
        {
            var response = _bussinessLogic.Get(name, idGenre, orderFilter, _context);
            if( response == null)return NotFound(response.ToString());
            return Ok(response);
        }
        //CREATE
        [HttpPost("create")]
        public IActionResult PostMovie(MovieSerie model)
        {
            var movieSerieCreate = _bussinessLogic.Create(model);
            _context.Add(movieSerieCreate);
            _context.SaveChanges();
            return Ok(movieSerieCreate);
        }
        //UPDATE
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] MovieSerie model)
        {
            var entityModify = _bussinessLogic.Update(id, model, _context);
            _context.Entry(entityModify).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(entityModify);
        }
        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie( int id )
        {
            var entityDeleted = _bussinessLogic.Delete( id, _context );
            _context.Remove(entityDeleted);
            _context.SaveChanges();
            return Ok();
        }
        //DETAILS
        [HttpGet("{id}")]
        public IActionResult DetailsMovie( int id )
        {
            var entityDetails = _bussinessLogic.Details(id, _context);
            if( entityDetails == null )return NotFound();
            return Ok( entityDetails );
        }
    }
}
