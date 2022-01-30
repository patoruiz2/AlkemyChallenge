using AlkemyChallenge.BL;
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
        private CharactersLogic _characterLogic = new CharactersLogic();


        public CharactersController(DbContextModel context)
        {
            _context = context;
        }
        //LIST
        [HttpGet]
        public ActionResult Get( [FromQuery] string name, [FromQuery] int? age = 0, [FromQuery] int movie = 0 )
        {
            var characterList = _characterLogic.Get( name, age, movie, _context );
            if( characterList == null ) return NotFound();
            return Ok( characterList );
        }
        //CREATE
        [HttpPost("send/character")]
        public IActionResult PostCharacter(Character model)
        {
            var characterCreated = _characterLogic.Create( model );
            _context.Add(characterCreated);
            _context.SaveChanges();
            return Ok(characterCreated);
        }
        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteCharacter(int id)
        {
            var characterDeleted = _characterLogic.Delete( id, _context );
            if(characterDeleted == null) return NotFound();
            return Ok(characterDeleted);
        }
        //UPDATE
        [HttpPut("{id}")]
        public IActionResult ModifyCharacter(int id,[FromBody] Character model)
        {
           var characterModify = _characterLogic.Update( id, model, _context );
           if (characterModify == null) return BadRequest();
           return Ok(characterModify);
        }
        //DETAILS
        [HttpGet("{id}")]
        public IActionResult DetailCharacter(int id)
        {
            var charactDetails = _characterLogic.Details(id, _context);
            return Ok(charactDetails);
        }
    }
}