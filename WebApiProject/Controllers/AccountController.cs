using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebApiProject.DTO;
using WebApiProject.Model;

namespace WebApiProject.Controllers
{
    [Authorize(AuthenticationSchemes ="Bearer")]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        /// <summary>
        /// Account Controller
        /// </summary>

        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        public AccountController(ILogger<AccountController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        /// <summary>
        /// Sign Up 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task SignUp([FromBody] NewuserDTO model)
        {
            var user = new AppUser { UserName = model.Username, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };

            var result = await _userManager.CreateAsync(user, "DefaultPassword@11@");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
            }

        }
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<string> SignIn([FromBody] SignInDTO model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
            if (result)
            {
                var email = new EmailDTO { Email = model.Email };
                var answer = GetToken(email);

                return answer.ToString();

            }
            return "Username or Password Incorrect";
        }

        /// <summary>
        /// Fetch All Register
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("FetchAllRegister")]
        public IActionResult FetchAllRegister()
        {
            var result = _userManager.Users;
            var users = new List<UserReturned>();

            foreach (var item in result)
            {
                var user = new UserReturned
                {
                    LastName = item.LastName,
                    FirstName = item.FirstName,
                    Email = item.Email
                };
                users.Add(user);
            }
            return Ok(users.ToList());

        }

        /// <summary>
        /// Fetch All Logged In User
        /// </summary>
        /// <returns></returns>
        [HttpGet("FetchAllLoggeInUser")]
        public async Task<IActionResult> FetchAllLoggeInUser()
        {
            
            var user = await _userManager.GetUserAsync(User);
            var result = new NewuserDTO { Email = user.Email, Username = user.UserName, FirstName = user.FirstName, LastName = user.LastName };
            return Ok(result);
        }

        /// <summary>
        /// Get Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("GetToken")]
        public string GetToken([FromBody] EmailDTO model)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);

            //Create claims for JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name, user.LastName)
            };

            //Get JWT secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            //Generate Signing Credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Create Security Token Descriptor
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
            };

            //Build Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //Create Token
            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            //return Token
            return tokenHandler.WriteToken(token);
        }
    }
} 
