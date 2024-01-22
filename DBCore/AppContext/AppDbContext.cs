using BrewTask.DBCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrewTask.DBCore.AppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BrewsEntity> Beers { get; set; }
        public DbSet<BrewRatingsEntity> Ratings { get; set; }
    }
}
