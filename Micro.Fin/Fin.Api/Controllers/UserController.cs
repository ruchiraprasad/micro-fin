using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Fin.Api.Models;
using Fin.ApplicationCore.Interfaces.Repositories;
using Fin.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public UserController(IOptions<AppSettings> appSettings, IUserRepository userRepository, IUserService userService)
        {
            this._appSettings = appSettings.Value;
            this._userRepository = userRepository;
            this._userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _userService.Authenticate(loginModel.UserName, loginModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: new List<Claim>() { new Claim(JwtRegisteredClaimNames.GivenName, loginModel.UserName) },
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            return new string[] { "value1", "value2" };
        }

        //// GET: api/User/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public object Get(int id)
        {
            var user = _userService.GetUser(id);
            var userModel = new
            {
                Id = user.Id,
                Username = user.UserName
            };

            return Ok(userModel);
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody]LoginModel loginModel)
        {
        }

        //// PUT: api/User/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
