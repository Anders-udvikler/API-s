using Library.RestApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.RestApi.Data
{
    ///the database session
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        // Tables in the database
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}