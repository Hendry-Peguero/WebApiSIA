
namespace WebApiSIA.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync();
        Task<Entity?> GetByIdAsync(int entityId);
        Task<Entity> UpdateAsync(Entity entity, int entityId);
    }
}
