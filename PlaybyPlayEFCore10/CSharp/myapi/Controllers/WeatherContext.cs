using Microsoft.EntityFrameworkCore;

namespace EFCoreWebAPI
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options)
        : base(options) { }

        public WeatherContext() { }

        public DbSet<WeatherEvent> WeatherEvents { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}