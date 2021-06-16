using CRUD.Net.App.Services;
using CRUD.Net.Domain.Apps;
using CRUD.Net.Domain.Notifications;
using CRUD.Net.Domain.Repositories;
using CRUD.Net.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD.Net.Infra.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //App
            services.AddScoped<IProdutoApp, ProdutoApp>();
            services.AddScoped<IFornecedorApp, FornecedorApp>();
            services.AddScoped<IUsuarioApp, UsuarioApp>();

            //Domain
            services.AddScoped<INotifier, Notifier>();

            //Infra Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
