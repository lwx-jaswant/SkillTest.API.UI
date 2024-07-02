
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Infrastructures.Data;
using SkillTest.Core.Infrastructures.Repository.Framwork;
using SkillTest.Core.IRepository;

namespace SkillTest.Core.Repositories.Impl
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(SkillTestDBContext context) : base(context)
        {

        }

        public Task<List<AppUser>> GetUserByEmail(string email)
        {
            IQueryable<AppUser> appUsers = _context.AppUsers.Where(x => x.Email == email).AsNoTracking();
            return appUsers.ToListAsync();
        }

        public Task<List<AppUser>> GetUserByRefreshToken(string refreshToken)
        {
            IQueryable<AppUser> appUsers = _context.AppUsers.Where(x => x.RefreshToken == refreshToken).AsNoTracking();

            return appUsers.ToListAsync();
        }

        public Task<AppUser?> GetById(int id)
        {
            IQueryable<AppUser> appUsers = _context.AppUsers.Where(x =>  x.Id == id);
            return appUsers.FirstOrDefaultAsync();
        }
    }
}
