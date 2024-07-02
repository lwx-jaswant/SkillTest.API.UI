using AutoMapper;
using MediatR;
using SkillTest.Core.Application._DTOs;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.IRepository.Framwork;

namespace SkillTest.Core.Application.Queries.AppUserQueries
{
    public class GetByIdAppUsersQueryHandler : IRequestHandler<GetByIdAppUsersQuery, AppUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdAppUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public  async Task<AppUserDto> Handle(GetByIdAppUsersQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _unitOfWork._appUserRepository.GetById(request.Id);
            AppUserDto userDto = _mapper.Map<AppUserDto>(user);
            return userDto;  
        }
    }
}
