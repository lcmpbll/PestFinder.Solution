using Microsoft.EntityFrameworkCore;

namespace PestFinder.Models
{
  public class PestFinderContext : DbContext
  {
    public DbSet<CLassOne> Pest { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<PestLocation> PestLocation { get;  set; }

    public PestFinderContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}