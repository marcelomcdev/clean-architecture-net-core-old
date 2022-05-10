using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToroBank.Core.Entities;
using ToroBank.Core.Repositories.Interfaces;

namespace ToroBank.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> UpdateAsync(User user)
        {
            user.Id = 1;
            return user;
        }

        public async Task<User> GetByCPFAsync(string cpf)
        {
            var user = new User(1000, "", "", 0);
            return user;

        }
    }
}
