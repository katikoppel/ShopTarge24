using Microsoft.Extensions.DependencyInjection;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.SpaceshipTest.Macros;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARge24.SpaceshipTest.Mock;

namespace ShopTARge24.SpaceshipTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; set; }


        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<ISpaceshipServices, SpaceshipServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IHostEnvironment, MockIHostEnvironment>();

            services.AddDbContext<ShopTARge24Context>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            RegisterMacros(services);
        }

        public void Dispose()
        {

        }

        protected T Svc<T>()
        {
            // Resolve service from the service provider
            return serviceProvider.GetService<T>();
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);

            var macros = macroBaseType.Assembly.GetTypes()
                .Where(t => macroBaseType.IsAssignableFrom(t)
                && !t.IsInterface && !t.IsAbstract);

            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }

    }
}