using Darewise_Challenge.DTOs;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Darewise_Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IDataProtector dataProtector;

        public AccountController(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

     
        [HttpPost("Register")] 
        public async Task<ActionResult<AuthResponse>> RegisterUser(UserCredentialsDTO userCredential)
        {
            var user = new IdentityUser
            {
                UserName = userCredential.Email,
                Email = userCredential.Email
            };
            var result = await userManager.CreateAsync(user, userCredential.Password);

            if (result.Succeeded)
            {
                return await BuildToken(userCredential);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(UserCredentialsDTO userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.Email,
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return BadRequest("Login Incorrect");
            }
        }

        private async Task<AuthResponse> BuildToken(UserCredentialsDTO userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("Email", userCredentials.Email)
            };

            var user = await userManager.FindByEmailAsync(userCredentials.Email);
            var claimsDB = await userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpirationTime = expiration
            };
        }

      
    }

}
