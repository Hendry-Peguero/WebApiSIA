using Microsoft.EntityFrameworkCore;
using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext context;

        public GenericRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await context.Set<Entity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<Entity> UpdateAsync(Entity entity, int entityId)
        {
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            context.Set<Entity>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await context.Set<Entity>().ToListAsync();
        }

        public virtual async Task<Entity?> GetByIdAsync(int entityId)
        {
            return await context.Set<Entity>().FindAsync(entityId);
        }
    }
}
