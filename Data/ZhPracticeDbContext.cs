using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZhPractice.Models;

namespace ZhPractice.Data
{
    public class ZhPracticeDbContext : IdentityDbContext
    {
        public ZhPracticeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StudentEntity> Student { get; set; }
    }
}
