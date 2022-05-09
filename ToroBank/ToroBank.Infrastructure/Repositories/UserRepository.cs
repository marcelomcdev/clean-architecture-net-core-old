using System.Collections.Generic;
using System.Threading.Tasks;
using ToroBank.Core.Entities;
using ToroBank.Core.Repositories.Interfaces;

namespace ToroBank.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> AddAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByCPFAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByCPFAsync(string cpf)
        {
            throw new System.NotImplementedException();
        }
    }
}
