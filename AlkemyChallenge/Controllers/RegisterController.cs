﻿using AlkemyChallenge.Helper;
using AlkemyChallenge.Model;
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
  
        public RegisterController(DbContextModel context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Post(RegisterUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
            }

            user.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            _context.Add(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}