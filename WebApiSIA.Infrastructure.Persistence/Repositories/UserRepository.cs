using Microsoft.EntityFrameworkCore;
using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Domain.Entities;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByUserNameAsync(string userName)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
