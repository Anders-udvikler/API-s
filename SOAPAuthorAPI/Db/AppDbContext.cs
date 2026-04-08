using Library.SoapApi.Db.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Book> tbook { get; set; }
    public DbSet<Author> tauthor { get; set; }
    public DbSet<Publishingcompany> tpublishingcompany { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}