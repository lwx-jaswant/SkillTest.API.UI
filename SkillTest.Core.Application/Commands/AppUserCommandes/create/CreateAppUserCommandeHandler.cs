using AutoMapper;
using MediatR;
using SkillTest.Core.Application._DTOs;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.IRepository.Framwork;

namespace SkillTest.Core.Application.Commands.AppUserCommandes.create
{
    public class CreateAppUserCommandeHandler : IRequestHandler<CreateAppUserCommande, AppUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateAppUserCommandeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppUserDto> Handle(CreateAppUserCommande request, CancellationToken cancellationToken)
        {
            AppUser appUserToAdd = _mapper.Map<AppUser>(request._appUserDto);
            AppUser addedAppUser = _unitOfWork._appUserRepository.Add(appUserToAdd);

            await _unitOfWork.saveAsync(cancellationToken);
            return _mapper.Map<AppUserDto>(addedAppUser);
        }
    }
}
