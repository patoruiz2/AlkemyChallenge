using AlkemyChallenge.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public ActionResult Get([FromQuery] string name, [FromQuery] int idGenre, [FromQuery] string orderFilter)
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
    }
}
