using System.Security.Cryptography;
using AutoMapper;
using MediatR;
using SkillTest.Core.Application._DTOs;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.IRepository.Framwork;

namespace SkillTest.Core.Application.Commands.AppUserCommandes.update
{
    public class UpdateAppUserCommandeHandler : IRequestHandler<UpdateAppUserCommande, AppUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAppUserCommandeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppUserDto> Handle(UpdateAppUserCommande request, CancellationToken cancellationToken)
        {
            AppUser dAppUserToUpdate = await _unitOfWork._appUserRepository.Get(request._appUserDto.Id);
            dAppUserToUpdate.FirstName = request._appUserDto.FirstName;
            dAppUserToUpdate.LastName = request._appUserDto.LastName;
            dAppUserToUpdate.Email = request._appUserDto.Email;
            dAppUserToUpdate.Role = request._appUserDto.Role;

            AppUser updatedAppUser = _unitOfWork._appUserRepository.Update(dAppUserToUpdate);
            await _unitOfWork.saveAsync(cancellationToken);

            return _mapper.Map<AppUserDto>(updatedAppUser);
        }
    }
}

