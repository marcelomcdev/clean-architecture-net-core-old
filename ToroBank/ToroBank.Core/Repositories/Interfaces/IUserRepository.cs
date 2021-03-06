using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Core.Entities;
using ToroBank.Core.Repositories.Interfaces.Base;

namespace ToroBank.Core.Repositories.Interfaces
{
    public interface IUserRepository //: IRepository<User>
    {
        User GetByCPFAsync(string cpf);
        Task<User> UpdateAsync(User user);
    }
}
