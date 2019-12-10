using InventarioAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventarioAPI.Controllers
{
    [Route("Api/v1/[controller]")]
    [ApiController]
    public class CuentasController: ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManger;
        private readonly IConfiguration _configuration;
        public CuentasController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signManger = signManager;
            this._configuration = configuration;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo userInfo)
        {//si al crear al usuario todo es exitoso se retornara un token 
            var user = new ApplicationUser { UserName = userInfo.Email, Email = userInfo.Email };
            var result = await _userManager.CreateAsync(user, userInfo.Password);
            if (result.Succeeded)
            {
                return BuildToken(userInfo); //token
            }
            else
            {
                return BadRequest("Username or password invalid");
            }
        }
        //metodo para descifrar el token
        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo user)
        {
            var result = await _signManger.PasswordSignInAsync(user.Email, user.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return BuildToken(user);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
            }
        }
        private UserToken BuildToken(UserInfo userInfo)
        {
            //claimers los cuales tiene una cierta configuracion del lado del token 

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("CualquierValor", "Valor de la llave"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );
            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
