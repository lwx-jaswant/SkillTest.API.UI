using AutoMapper;
using MediatR;
using SkillTest.Core.Domain.IRepository.Framwork;

namespace SkillTest.Core.Application.Commands.AppUserCommandes.delete
{
    public class DeleteAppUserCommandeHandler : IRequestHandler<DeleteAppUserCommande, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteAppUserCommandeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteAppUserCommande request, CancellationToken cancellationToken)
        {
            _unitOfWork._appUserRepository.Delete(request.Id);
            await _unitOfWork.saveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
