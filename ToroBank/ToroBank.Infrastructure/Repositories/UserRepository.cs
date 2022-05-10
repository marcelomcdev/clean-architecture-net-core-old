using System.Linq;
using System.Threading.Tasks;
using ToroBank.Core.Repositories.Interfaces;
using ToroBank.Infrastructure.Context;
using ToroBank.Infrastructure.Data.Entities;
using ToroBank.Infrastructure.Repositories.Base;

namespace ToroBank.Infrastructure.Repositories
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BaseContext context) : base(context)
        {
        }

        public Core.Entities.User GetByCPFAsync(string cpf)
        {
            return base.FindBy(f => f.CPF == cpf).FirstOrDefault();
        }

        public async Task<Core.Entities.User> UpdateAsync(Core.Entities.User user)
        {
            return await base.UpdateAsync((User)user);
        }
    }
}
