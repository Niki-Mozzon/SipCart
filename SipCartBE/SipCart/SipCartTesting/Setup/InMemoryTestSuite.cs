using Microsoft.EntityFrameworkCore;
using AppContext = SipCartCore.AppContext;


namespace SipCartTesting.Setup
{
    public abstract class InMemoryTestSuite
    {
        protected SipCartCore.AppContext _appContext;

        protected async Task GetContextAsync()
        {
            DbContextOptions<SipCartCore.AppContext> options = new DbContextOptionsBuilder<AppContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _appContext = new AppContext(options);
            await _appContext.Database.EnsureCreatedAsync();
        }

        protected async Task CleanUpAsync()
        {
            await _appContext.Database.EnsureDeletedAsync();
            await _appContext.DisposeAsync();
        }
    }
}
