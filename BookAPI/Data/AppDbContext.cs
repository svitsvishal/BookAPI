using Microsoft.EntityFrameworkCore;
using BookAPI.Models;

namespace BookAPI.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
