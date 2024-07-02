using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.IRepository.Framwork;

namespace SkillTest.Core.IRepository
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        public Task<List<AppUser>> GetUserByEmail(string email);
        public Task<List<AppUser>> GetUserByRefreshToken(string property);
        public Task<AppUser?> GetById(int id);
    }
}
