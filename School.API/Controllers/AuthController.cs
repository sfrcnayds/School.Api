using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using School.API.Dtos;
using School.API.Models;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SchoolContext _context;

        public AuthController(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("loginStudent")]
        public async Task<ActionResult> LoginStudent([FromBody] StudentForLoginDto loginUser)
        {
            var users = await _context.Student
                .Where(s => s.Number == loginUser.number && s.Password == loginUser.password).ToListAsync();
            if (users.Count == 0) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var user = users.First();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Mail),
                    new Claim(ClaimTypes.Name, user.Name + " " + user.Surname)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var response = new LoginStudentResponse
            {
                Id = user.Id,
                Name = user.Name + " " + user.Surname,
                Token = tokenString
            };
            return Ok(response);
        }

        [HttpPost("loginTeacher")]
        public async Task<ActionResult> LoginTeacher([FromBody] TeacherForLogin loginTeacher)
        {
            var teachers = await _context.Teacher
                .Where(t => t.Email == loginTeacher.Email && t.Password == loginTeacher.Password).ToListAsync();
            if (teachers.Count == 0) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var teacher = teachers.First();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, teacher.Id.ToString()),
                    new Claim(ClaimTypes.Email, teacher.Email),
                    new Claim(ClaimTypes.Name, teacher.Name + " " + teacher.Surname)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var response = new LoginStudentResponse
            {
                Id = teacher.Id,
                Name = teacher.Name + " " + teacher.Surname,
                Token = tokenString
            };
            return Ok(response);
        }
    }
}