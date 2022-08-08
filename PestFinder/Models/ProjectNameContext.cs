using Microsoft.EntityFrameworkCore;

namespace PestFinder.Models
{
  public class PestFinderContext : DbContext
  {
    public DbSet<CLassOne> ClassOne { get; set; }
    public DbSet<ClassTwo> ClassTwo { get; set; }
    public DbSet<ClassOneClassTwo> ClassOneClassTwo { get;  set; }

    public PestFinderContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}