using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkillTest.Core.IRepository;

namespace SkillTest.Core.Domain.IRepository.Framwork
{
    public interface IUnitOfWork : IDisposable 
    {
        IAppUserRepository _appUserRepository { get; }
        int save();
        Task<int> saveAsync(CancellationToken cancellationToken);
        public void detach(object entry);
        public void attach(object entry);
        public void Modif(object entry);

    }
}
