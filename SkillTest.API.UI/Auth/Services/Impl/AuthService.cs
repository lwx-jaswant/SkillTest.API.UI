﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SkillTest.API.UI.Auth;
using SkillTest.API.UI.Util;
using SkillTest.Core.Application._DTOs;
using SkillTest.Core.Domain.Entity;

namespace SkillTest.Core.Application.Services.Impl
{
    public class AuthService : Services.IAuthService
    {
        private IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string CreateToken(AppUserDto userDto, string myRole)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDto.Email),
                new Claim(ClaimTypes.Role, myRole),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }
    }
}
