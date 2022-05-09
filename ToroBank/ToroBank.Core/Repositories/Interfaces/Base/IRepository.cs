using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToroBank.Core.Repositories.Interfaces.Base
{
    public interface IRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T user);
        Task<T> UpdateAsync(T user);
        Task<T> DeleteAsync(int id);

    }
}
