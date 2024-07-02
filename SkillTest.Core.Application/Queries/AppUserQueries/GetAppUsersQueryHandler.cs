using AutoMapper;
using MediatR;
using SkillTest.Core.Application._DTOs;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.IRepository.Framwork;

namespace SkillTest.Core.Application.Queries.AppUserQueries
{
    public class GetAppUsersQueryHandler : IRequestHandler<GetAppUsersQuery, List<AppUserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAppUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public  async Task<List<AppUserDto>> Handle(GetAppUsersQuery request, CancellationToken cancellationToken)
        {
            List<AppUser> users = new List<AppUser>();
            List<AppUserDto> usersDto = new List<AppUserDto>(); ;
            switch (request.PropertyName)
            {
                case AppUsersCrieriaEnum.All:
                    {
                        users = await _unitOfWork._appUserRepository.GetAll();
                        break;
                    }
                case AppUsersCrieriaEnum.ByEmail:
                    {
                        users = await _unitOfWork._appUserRepository.GetUserByEmail((string)request.PropertyValue);
                        break;
                    }
                case AppUsersCrieriaEnum.ByRefreshToken:
                    {
                        users = await _unitOfWork._appUserRepository.GetUserByRefreshToken((string)request.PropertyValue);
                        break;
                    }
                default: break;

            }

            usersDto = users.Select(u => _mapper.Map<AppUserDto>(u)).ToList();
            return usersDto;  
        }
    }
    public enum AppUsersCrieriaEnum
    {
        All,
        ByCompany,
        ByEmail,
        ByFirstName,
        ByLastName,
        ByRefreshToken
    }
}
