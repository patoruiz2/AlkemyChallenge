using AlkemyChallenge.Helper;
using AlkemyChallenge.Model;
using AlkemyChallenge.Model.ViewModel;
using AlkemyChallenge.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AlkemyChallenge.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginLogic _loginLogic = new LoginLogic();
        private readonly DbContextModel _context;
        public LoginController(DbContextModel context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Post( LoginViewModel model )
        {
            if ( !ModelState.IsValid )return BadRequest(ErrorHelper.GetModelStateErrors( ModelState ));
            var user = _loginLogic.LogIn( model, _context );
            if ( user == null )return NotFound ( new { Message = "User not Found" } );
            return Ok( user );
        }
    }
}
