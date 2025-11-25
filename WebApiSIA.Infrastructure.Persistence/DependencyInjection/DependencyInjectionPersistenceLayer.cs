using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiSIA.Infrastructure.Persistence.Contexts;
using WebApiSIA.Infrastructure.Persistence.Repositories;

namespace WebApiSIA.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjectionPersistenceLayer
    {
        public static void AddPersistenceDependency(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(
                    connectionString,
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                )
            );



            #region Repositories
            services.AddScoped<InventoryMovementRepository>();
            //services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddTransient<IImprovementRepository, ImprovementRepository>();
            //services.AddTransient<IPropertyRepository, PropertyRepository>();
            //services.AddTransient<IPropertyFavoriteRepository, PropertyFavoriteRepository>();
            //services.AddTransient<IPropertyImageRepository, PropertyImageRepository>();
            //services.AddTransient<IPropertyImprovementRepository, PropertyImprovementRepository>();
            //services.AddTransient<IPropertyTypeRepository, PropertyTypeRepository>();
            //services.AddTransient<ISaleTypeRepository, SaleTypeRepository>();

            #endregion
        }
    }
}
