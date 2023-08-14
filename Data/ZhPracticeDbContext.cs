using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZhPractice.Models;
using ZhPractice.Models.Question.Question;
using ZhPractice.Models.Module;
using ZhPractice.Models.Answer;

namespace ZhPractice.Data
{
    public class ZhPracticeDbContext : IdentityDbContext
    {
        public ZhPracticeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Question> Question { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
