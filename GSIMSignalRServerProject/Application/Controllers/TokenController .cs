﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GSIMSignalRServerProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GSIMSignalRServerProject.Application.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class TokenController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult GetToken([FromBody] LoginModel login)
        {
            if (!ValidateUser(login)) return Unauthorized();
            var tokenString = GenerateJwtToken(login.Username);
            return Ok(new { Token = tokenString });

        }
        
        private bool ValidateUser(LoginModel login)
        {
            // For demonstration, using hardcoded validation
            // In production, you should validate against a database or another reliable source
            return login is { Username: "sysadmin", Password: "masterkey" }; // Replace with real validation
        }
        
        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SvNhHG3PMx_ql8g_mwwX4QXa_dQlqJjjpGgSjXKrB80\n")); // Replace with your secret key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                
                issuer: "yourdomain.com", // Replace with your domain
                audience: "yourdomain.com", // Replace with your domain
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
