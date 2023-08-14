using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZhPractice.Data;

namespace ZhPractice.Controllers
{
    public class Practice : Controller
    {
        private readonly ZhPracticeDbContext zhContext;

        public Practice(ZhPracticeDbContext zhContext)
        {
            this.zhContext = zhContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return zhContext.Module != null ? View(await zhContext.Module.ToListAsync()) : Problem("Üres a zh context");
        }
    }
}
