using Microsoft.EntityFrameworkCore;
using Auth.Models;

namespace Auth.Data
{
    public class User : DbContext
    {
        public User(DbContextOptions<User> options)
            : base(options)
        {
        }

        public DbSet<UserReport> UserAdditionalDatas { get; set; }
    }
}