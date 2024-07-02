using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace SkillTest.API.UI.Controllers
{
    public abstract class AbstractContoller : ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        public JwtSecurityToken getToken()
        {
            string jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("bearer ", "").Replace("Bearer ", "");
            var token = new JwtSecurityToken(jwt);
            return token;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string getClaim(string claimType)
        {
            var claim = getToken().Claims.First(claim => claim.Type == claimType).Value;
            return claim;
        }
    }
}
