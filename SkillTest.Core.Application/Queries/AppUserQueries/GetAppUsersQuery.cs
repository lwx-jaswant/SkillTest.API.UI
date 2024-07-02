using MediatR;
using SkillTest.Core.Application._DTOs;

namespace SkillTest.Core.Application.Queries.AppUserQueries
{
    public class GetAppUsersQuery : IRequest<List<AppUserDto>>
    {
        public AppUsersCrieriaEnum PropertyName { get; set; }
        public Object PropertyValue { get; set; }
    }

   
}
