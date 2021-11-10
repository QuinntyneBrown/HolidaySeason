using HolidaySeason.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace HolidaySeason.Api.Interfaces
{
    public interface IHolidaySeasonDbContext
    {
        DbSet<HolidayEvent> HolidayEvents { get; }
        DbSet<StoredEvent> StoredEvents { get; }
        DbSet<User> Users { get; }
        DbSet<DigitalAsset> DigitalAssets { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
