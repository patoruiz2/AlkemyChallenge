using AlkemyChallenge.Model;
using AlkemyChallenge.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                return BadRequest(ModelState.Values);
            }
            return Ok();
        }
    }
}
