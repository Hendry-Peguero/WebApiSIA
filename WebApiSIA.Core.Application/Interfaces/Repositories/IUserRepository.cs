using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity?> GetByUserNameAsync(string userName);
    }
}