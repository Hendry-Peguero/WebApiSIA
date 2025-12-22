using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiSIA.Core.Application.Interfaces.Helpers;
using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Infrastructure.Persistence.Contexts;
using WebApiSIA.Infrastructure.Persistence.Helpers;
using WebApiSIA.Infrastructure.Persistence.Repositories;

namespace WebApiSIA.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjectionPersistenceLayer
    {
        public static void AddPersistenceDependency(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");


            services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(
                    connectionString,
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                )
            );



            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IInventoryMovementRepository, InventoryMovementRepository>();
            services.AddTransient<IItemInformationRepository, ItemInformationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IItemGroupRepository, ItemGroupRepository>();
            services.AddTransient<IVatRespository, VatRespository>();
            services.AddTransient<IWareHouseRepository, WareHouseRepository>();
            #endregion

            #region Helpers
            services.AddTransient<ISqlHelper, SqlHelper>();
            #endregion
        }
    }
}
