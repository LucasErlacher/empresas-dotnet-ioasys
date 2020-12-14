using Ioasys.IMDb.API.Configurations;
using Ioasys.IMDb.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ioasys.IMDb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("logar")]
        public async Task<ActionResult> Logar(InfoUserViewModel loginUser)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Senha, false, true);

            if (result.Succeeded)
            {
                return Ok(await GerarJwt(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                return BadRequest(new { Errors = new[] { "LockedOut" } });
            }

            return Ok(loginUser);
        }

        [HttpPut("ativar-usuario")]
        public virtual async Task<ActionResult> Ativar(InfoUserViewModel infoUser)
        {
            var user = await _userManager.FindByEmailAsync(infoUser.Email);

            if (user.LockoutEnabled)
            {
                var result = await _userManager.SetLockoutEnabledAsync(user, false);

                if (!result.Succeeded)
                    return BadRequest(new { Errors = result.Errors });
            }

            return Ok();
        }

        [HttpPut("desativar-usuario")]
        public virtual async Task<ActionResult> Desativar(InfoUserViewModel infoUser)
        {
            var user = await _userManager.FindByEmailAsync(infoUser.Email);

            if (!user.LockoutEnabled)
            {
                var result = await _userManager.SetLockoutEnabledAsync(user, true);

                if (!result.Succeeded)
                    return BadRequest(new { Errors = result.Errors });
            }

            return Ok();
        }


        protected async Task<ActionResult> RegistrarUsuario(InfoUserViewModel infoUser)
        {
            var user = new IdentityUser
            {

                UserName = infoUser.Email,
                Email = infoUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, infoUser.Senha);

            if (!result.Succeeded)
                return BadRequest(new { result.Errors });

            if (infoUser.Role == RoleType.ADMINISTRADOR)
                await _userManager.AddToRoleAsync(user, "Administrador");
            else
                await _userManager.AddToRoleAsync(user, "Usuario");

            await _signInManager.SignInAsync(user, false);

            return Ok();
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds
            };

            return response;
        }
    }
}
