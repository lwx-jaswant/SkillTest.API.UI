using System;
using SkillTest.API.UI.Auth;
using SkillTest.Core.Application._DTOs;

namespace SkillTest.Core.Application.Services
{
    public interface IAuthService
    {
        public string CreateToken(AppUserDto user, string myRole);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public RefreshToken GenerateRefreshToken();
    }
}
