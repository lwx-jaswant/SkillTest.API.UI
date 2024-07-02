using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkillTest.Core.Application._DTOs
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }

        public string Role { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; } = string.Empty;
        [JsonIgnore]
        public DateTime? TokenCreated { get; set; }
        [JsonIgnore]
        public DateTime? TokenExpires { get; set; }
    }
}
