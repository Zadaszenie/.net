using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using VisacomWepApp.Models;
using VisacomWepApp.Data;
using VisacomWepApp.Dtos;
using VisacomWepApp.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace VisacomWepApp.Controllers
{
    [Route(template:"api")]
    [ApiController]
    public class AuthController: Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }
        [HttpPost(template:"register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _repository.Create(user);
            return Created(uri: "success", value: _repository.Create(user));
        }
        [HttpPost(template:"login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _repository.GetByEmailAsync(dto.Email);

            if (user == null) return BadRequest(error: new { message = "invalid email or password" });

            if(!BCrypt.Net.BCrypt.Verify(dto.Password, hash:user.Password))
            {
                return BadRequest(error: new { message = "invalid email or password" });
            }

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append(key: "jwt", value: jwt, new CookieOptions
            {
                HttpOnly = true
            }) ;
            return Ok(new 
            { 
                message = "success"
            });
        }
        [HttpGet(template:"user")]
            //[Route("Login")]
            //[EnableCors("AllowOrigin")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = _repository.GetById(userId);

                return Ok(user);
            }catch(Exception e)
            {
                return Unauthorized();
            }

        }

        [HttpPost(template:"Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(key: "jwt");

            return Ok(new
            {
                message = "success"
            });
                
        }

    }
}
