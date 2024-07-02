using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillTest.Core.Domain.IRepository;
using SkillTest.Core.Domain.IRepository.Framwork;
using SkillTest.Core.Infrastructures.Data;
using SkillTest.Core.IRepository;

namespace SkillTest.Core.Infrastructures.Repository.Framwork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SkillTestDBContext _context;
        public IAppUserRepository _appUserRepository { get; }
        

        public UnitOfWork(SkillTestDBContext skillTestDBContext, IAppUserRepository appUserRepository   )
        {
            _context = skillTestDBContext;
            _appUserRepository = appUserRepository;

        }

        public void detach(object entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void attach(object entity)
        {
            _context.Entry(entity).State = EntityState.Unchanged;
        }

        public void Modif(object entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public int save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> saveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
