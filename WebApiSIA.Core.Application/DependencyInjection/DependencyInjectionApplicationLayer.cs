using Microsoft.Extensions.DependencyInjection;
using WebApiSIA.Core.Application.Interfaces.Services;
using WebApiSIA.Core.Application.Mappings;
using WebApiSIA.Core.Application.Services;

namespace WebApiSIA.Core.Application.DependencyInjection
{
    public static class DependencyInjectionApplicationLayer
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));


            services.AddAutoMapper(typeof(GeneralProfile).Assembly);
            //services.AddTransient<IJsonHelper, JsonHelper>();
        }
    }
}
