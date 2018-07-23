using Banicharnica.Data;
using Banicharnica.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Banicharnica.Services
{
    public class DbInitizlizerService : IDbInitizlizerService
    {
        private readonly BanicharnicaContext context;
        public DbInitizlizerService(BanicharnicaContext context)
        {
            this.context = context;
        }
        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}