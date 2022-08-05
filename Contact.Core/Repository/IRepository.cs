using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Core.Repository
{
    public interface IRepository<T> where T : class
    {
        T Find(object Id);
        T Find(params object[] keyValues);
        public Task<T> FindAsync(object Id);
        public IQueryable<T> GetAll();
        public T GetById(params object[] keyValues);
        public Task<T> AddAsync(T entity);
        public Task<List<T>> AddRangeAsync(List<T> entity);
        public Task<T> UpdateAsync(T entity);
        public Task<List<T>> UpdateRangeAsync(List<T> entity);
        public IQueryable<T> Queryable();
        public IQueryable<T> QueryableAsNoTracking();
        public bool Delete(T entity);
        public bool DeleteById(object id);
        public bool SaveNow();
        public Task<bool> SaveNowAsync();
    }
}
