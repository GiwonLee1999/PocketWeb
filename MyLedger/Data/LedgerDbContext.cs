using Microsoft.EntityFrameworkCore;
using MyLedger.Models.Account;

namespace MyLedger.Web.Data
{
    public class LedgerDbContext : DbContext
    {
        public LedgerDbContext(DbContextOptions<LedgerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Amount> Amounts { get; set; }
    }
}
