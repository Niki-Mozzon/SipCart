using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SipCartApi;
using Testcontainers.MsSql;

namespace SipCartTesting.Setup
{

    internal sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly MsSqlContainer? _sqlContainer;

        public CustomWebApplicationFactory(MsSqlContainer sqlContainer)
        {
            _sqlContainer = sqlContainer;
        }

        public CustomWebApplicationFactory()
        {
        }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                if (_sqlContainer == null)
                {
                    return;
                }
                ServiceDescriptor? descriptor = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<SipCartCore.AppContext>));
                services.Remove(descriptor);
                services.AddDbContext<SipCartCore.AppContext>((_, option) => option.UseSqlServer(_sqlContainer.GetConnectionString()));
            });
        }
    }
}
