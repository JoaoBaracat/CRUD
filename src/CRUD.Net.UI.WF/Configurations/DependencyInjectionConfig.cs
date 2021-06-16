using CRUD.Net.Infra.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CRUD.Net.UI.WF.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
