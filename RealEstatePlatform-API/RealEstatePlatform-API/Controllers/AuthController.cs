using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstatePlatform_API.DTOs.Auth;
using RealEstatePlatform_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstatePlatform_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManger,
                              IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManger = roleManger;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var userExistes = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (userExistes != null)
            {
                return BadRequest(new { Message = "USER ALREDY EXISTS !" });
            }

            var user = new ApplicationUser
            {
                UserName = registerDTO.FullName,
                FullName = registerDTO.FullName,
                Email = registerDTO.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            if (!await _roleManger.RoleExistsAsync(registerDTO.Role))
                await _roleManger.CreateAsync(new IdentityRole(registerDTO.Role));

            await _userManager.AddToRoleAsync(user, registerDTO.Role);

            return Ok(new {Message = "USER CREATED SUCCESSFULLY!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> login (LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync (loginDTO.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return Unauthorized("Invalid Credentails");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,loginDTO.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            });
        }
    }
}
