using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.API.UI.ExtentionMethods;
using SkillTest.Core.Application._DTOs;
using SkillTest.Core.Application.Commands.AppUserCommandes.create;
using SkillTest.Core.Application.Commands.AppUserCommandes.update;
using SkillTest.Core.Application.Queries.AppUserQueries;
using SkillTest.Core.Application.Services;

namespace SkillTest.API.UI.Auth
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;


        public AuthController(ILogger<AuthController> logger, IMediator mediator, IAuthService authService)
        {
            this._logger = logger;
            this._mediator = mediator;
            this._authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterDto>> Register(RegisterDto registerDto)
        {
            this._authService.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            AppUserDto appUserDto = new AppUserDto(){
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "Admin"
            };



            AppUserDto  result = await this._mediator.Send(new CreateAppUserCommande { _appUserDto = appUserDto });
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            List<AppUserDto> results = await this._mediator.Send(new GetAppUsersQuery() { PropertyName = AppUsersCrieriaEnum.ByEmail, PropertyValue = loginDto.Email });
            if (results.Count == 0)
            {
                return BadRequest("User not found.");
            }
            if (results.Count > 1)
            {
                return BadRequest("Error : Many users found.");
            }
            AppUserDto appUserDto = results[0]; 
            if (!this._authService.VerifyPasswordHash(loginDto.Password, appUserDto.PasswordHash, appUserDto.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            // get user by email et recuperer le role de la BDD
            string token = this._authService.CreateToken(appUserDto, appUserDto.Role);

            var refreshToken = this._authService.GenerateRefreshToken();
            await SetRefreshTokenAsync(appUserDto, refreshToken);

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            List<AppUserDto> results = await this._mediator.Send(new GetAppUsersQuery() { PropertyName = AppUsersCrieriaEnum.ByRefreshToken, PropertyValue = refreshToken });
            if (results.Count != 1)
            {
                return Unauthorized("Invalid Refresh Token.");
            }

            AppUserDto appUserDto = results[0];
            if (appUserDto.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = this._authService.CreateToken(appUserDto, appUserDto.Role);
            var newRefreshToken = this._authService.GenerateRefreshToken();
            await SetRefreshTokenAsync(appUserDto, newRefreshToken);

            return Ok(token);
        }

        private async Task SetRefreshTokenAsync(AppUserDto appUserDto, RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            appUserDto.RefreshToken = newRefreshToken.Token;
            appUserDto.TokenCreated = newRefreshToken.Created;
            appUserDto.TokenExpires = newRefreshToken.Expires;

            AppUserDto result = await this._mediator.Send(new UpdateAppUserCommande() { _appUserDto = appUserDto });
        }
    }
}
