using AlkemyChallenge.BL;
using AlkemyChallenge.Helper;
using AlkemyChallenge.Model;
using AlkemyChallenge.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AlkemyChallenge.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly DbContextModel _context;
        private RegisterLogic _registerLogic = new RegisterLogic();
        public RegisterController(DbContextModel context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Post( RegisterUser model )
        {
            if (!ModelState.IsValid) return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
            var user = _registerLogic.Register(model, _context);
            return Ok(user);
        }
    }
}
