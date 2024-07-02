using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SkillTest.Core.Application._DTOs;

namespace SkillTest.Core.Application.Queries.AppUserQueries
{
    public class GetByIdAppUsersQuery : IRequest<AppUserDto>
    {
        public int Id { get; set; }

    }
}
