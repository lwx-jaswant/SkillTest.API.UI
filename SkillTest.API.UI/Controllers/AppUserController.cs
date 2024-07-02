using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.API.UI.ExtentionMethods;
using SkillTest.API.UI.Enums;
using SkillTest.API.UI.Util;
using SkillTest.Core.Application._DTOs;
using SkillTest.Core.Application.Commands.AppUserCommandes;
using SkillTest.Core.Application.Commands.AppUserCommandes.create;
using SkillTest.Core.Application.Commands.AppUserCommandes.delete;
using SkillTest.Core.Application.Commands.AppUserCommandes.update;
using SkillTest.Core.Application.Queries.AppUserQueries;
using SkillTest.Core.Application.Services;

namespace SkillTest.API.UI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppUserController : AbstractContoller
    {

        private readonly ILogger<AppUserController> _logger;
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public AppUserController(ILogger<AppUserController> logger, IMediator mediator, IAuthService authService)
        {
            this._logger = logger;
            this._mediator = mediator;
            this._authService = authService;
        }

        [Route("")]
        [HttpGet, Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetAll()
        {
            string role = getClaim(ClaimTypes.Role);
            var results = new List<AppUserDto> ();
            if (RoleEnum.Admin.ToString().Equals(role))
            {
                results = await this._mediator.Send(new GetAppUsersQuery() { PropertyName = AppUsersCrieriaEnum.All });
            }


            return Ok(results);
        }

        [Route("{id:int}")]
        [HttpGet, Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetById(int id)
        {
            string role = getClaim(ClaimTypes.Role);

            AppUserDto result = await this._mediator.Send(new GetByIdAppUsersQuery() { Id = id });
            if(result == null)
            {
                return this.StatusCode(StatusCodes.Status204NoContent, "AppUser Not Found");
            }
  
            return Ok(result);
        }

        [Route("")]
        [HttpPost, Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Create(RegisterDto registerDto)
        {
            this._authService.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            AppUserDto appUserDto = new AppUserDto()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = registerDto.Role
            };

            

            AppUserDto result = await this._mediator.Send(new CreateAppUserCommande() { _appUserDto = appUserDto });
            return Ok(result);
        }

        [Route("{id:int}")]
        [HttpPut, Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Edit(int id, AppUserDto appUserDto)
        {
            if (id != appUserDto.Id)
            {
                return BadRequest("Different id between rueqest and body");
            }

            string role = getClaim(ClaimTypes.Role);

            if (!RoleEnum.Admin.ToString().Equals(role))
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, "You do not have the right to modify this user");
            }

            AppUserDto result = await this._mediator.Send(new UpdateAppUserCommande() { _appUserDto = appUserDto });
            return Ok(result);

        }

        [HttpDelete, Authorize(Roles = "Admin, SuperAdmin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            string role = getClaim(ClaimTypes.Role);
            AppUserDto existingAppUserDto = await this._mediator.Send(new GetByIdAppUsersQuery() { Id = id });
            if (existingAppUserDto == null)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "AppUser Not Found");
            }
            if(!RoleEnum.Admin.ToString().Equals(role))
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, "You do not have the right to delete this user");
            }

            await this._mediator.Send(new DeleteAppUserCommande() { Id = id});
            return Ok();
        }
    }
}