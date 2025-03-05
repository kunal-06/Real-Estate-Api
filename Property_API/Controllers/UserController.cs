using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Property_API.Data;
using Property_API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;

namespace Property_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserRepo _userRepo;
        public IConfiguration _configuration { get; set; }
        public UserController(UserRepo _userRepo,IConfiguration configuration)
  
        {
            this._userRepo = _userRepo;
            this._configuration = configuration;
        }


        [HttpGet]
        
        public IActionResult getAll()
        {
            var users = _userRepo.GetAllUser();
            return Ok(users);
        }

        [HttpGet("{UserID}")]
        [Authorize]
        public IActionResult getByID(int UserID)
        {

            var cities = _userRepo.GetUserByID(UserID);

            return Ok(cities);

        }

        [HttpDelete(("{UserID}"))]
        [Authorize]
        public IActionResult deleteByID(int UserID)
        {

            var users = _userRepo.DeleteUser(UserID);
            if (users)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPost]
        public IActionResult InsertCity([FromBody] UserModel userModel)
        {
            bool is_Insrted = _userRepo.InsertUser(userModel);

            if (!is_Insrted)
            {
                return BadRequest();
            }
            return Ok("Inserted");
        }

        [HttpPut(("{UserID}"))]
        [Authorize]
        public IActionResult UpdateUser([FromBody] UserModel userModel)
        {
            bool is_Updated = _userRepo.UpdateUser(userModel);

            if (!is_Updated)
            {
                return BadRequest();
            }
            return Ok("Updated");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin userLoginModel)
        {

            var userData = _userRepo.AuthenticateUser(userLoginModel);
            if (userLoginModel == null)
            {
                return BadRequest();
            }
            
            if (userData != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ),
                    new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
                    new Claim("UserID", userData.UserID.ToString()),
                    new Claim("Email", userData.Email.ToString()),
                    new Claim("UserName", userData.UserName.ToString()),
                    new Claim("Phone", userData.Phone.ToString()),
                    new Claim("Role", userData.Role.ToString()),

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: signIn
                    );

                string tockenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tockenValue, User = userData, Message = "User Login Successfully",status=200});
            }
            return Unauthorized("Invalid username or password or Role.");
        }

    }
}
