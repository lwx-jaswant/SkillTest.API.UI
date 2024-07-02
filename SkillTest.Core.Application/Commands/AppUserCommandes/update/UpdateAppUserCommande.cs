using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SkillTest.Core.Application._DTOs;

namespace SkillTest.Core.Application.Commands.AppUserCommandes.update
{
    public class UpdateAppUserCommande : IRequest<AppUserDto>
    {
        public AppUserDto _appUserDto { get; set; }
    }
}
