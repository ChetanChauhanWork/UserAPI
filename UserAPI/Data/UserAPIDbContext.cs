using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Data
{
    public class UserAPIDbContext : DbContext
    {
        public UserAPIDbContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<User> Users { get; set; }
    }
}
