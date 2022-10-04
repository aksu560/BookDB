using BookDB.Models;
using Microsoft.EntityFrameworkCore;

namespace BookDB.Data
{
    public class BookDBContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public BookDBContext(DbContextOptions<BookDBContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(configuration.GetConnectionString("BookDatabase"));
        }

        public DbSet<Book> Books { get; set; }
    }
}
