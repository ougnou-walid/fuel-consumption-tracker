
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace fuel_consumption_tracker.DAL;

public class DbContextApp : DbContext
{
    public DbContextApp(DbContextOptions<DbContextApp> options) : base(options)
    {
    }
}
