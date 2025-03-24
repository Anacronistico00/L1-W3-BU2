using L1_W3_BU2.Models;
using Microsoft.EntityFrameworkCore;

namespace L1_W3_BU2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
