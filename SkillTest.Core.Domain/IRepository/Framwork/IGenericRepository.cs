using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTest.Core.Domain.IRepository.Framwork
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<List<T>> GetAll();
        T Add(T entity);
        void Delete(T entity);
        void Delete(int id);
        T Update(T entity);
    }
}
