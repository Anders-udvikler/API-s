using Library.SoapApi.Db.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<tbook> tbook { get; set; }
    public DbSet<tauthor> tauthor { get; set; }
    public DbSet<tpublishingcompany> tpublishingcompany { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}