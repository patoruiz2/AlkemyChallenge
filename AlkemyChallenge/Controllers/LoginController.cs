using AlkemyChallenge.Helper;
using AlkemyChallenge.Model;
using AlkemyChallenge.Model.ViewModel;
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
        private readonly DbContextModel _context;

        public LoginController(DbContextModel context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
            }
            RegisterUser registerUser = _context.RegisterUsers.Where(x => x.UserName == user.UserName)
                .Where(t => t.Token != null)
                .Where(p => p.Password == user.Password)
                .FirstOrDefault();

            if (registerUser == null)
            {
                return NotFound(new {Message = "User not Found"});
            }
            return Ok(registerUser.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
        }
    }
}
