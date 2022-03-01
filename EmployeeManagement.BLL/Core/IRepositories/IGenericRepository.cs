using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.BLL.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
         Task<IEnumerable<T>> All();

         Task<T> GetById(string id);

         Task<bool> DeleteById(string id);

         Task<T> Add(T entity);

         Task<bool> Upsert(T entity);
    }
}