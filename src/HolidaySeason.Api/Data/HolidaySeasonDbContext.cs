using HolidaySeason.Api.Models;
using HolidaySeason.Api.Core;
using HolidaySeason.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace HolidaySeason.Api.Data
{
    public class HolidaySeasonDbContext: DbContext, IHolidaySeasonDbContext
    {
        public DbSet<HolidayEvent> HolidayEvents { get; private set; }
        public DbSet<StoredEvent> StoredEvents { get; private set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<DigitalAsset> DigitalAssets { get; private set; }
        public HolidaySeasonDbContext(DbContextOptions options)
            {
                SavingChanges += DbContext_SavingChanges;
            }

        private void DbContext_SavingChanges(object sender, SavingChangesEventArgs e)
        {
            var entries = ChangeTracker.Entries<AggregateRoot>()
                .Where(
                    e => e.State == EntityState.Added ||
                    e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .ToList();
            
            foreach (var aggregate in entries)
            {
                foreach (var storedEvent in aggregate.StoredEvents)
                {
                    StoredEvents.Add(storedEvent);
                }
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HolidaySeasonDbContext).Assembly);
        }
        
        public override void Dispose()
        {
            SavingChanges -= DbContext_SavingChanges;
            
            base.Dispose();
        }
        
        public override ValueTask DisposeAsync()
        {
            SavingChanges -= DbContext_SavingChanges;
            
            return base.DisposeAsync();
        }
        
    }
}
