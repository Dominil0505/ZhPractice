using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ZhPractice.Data;
using ZhPractice.Models.Module;

namespace ZhPractice.Controllers
{
    public class ModulesController : Controller
    {
        private readonly ZhPracticeDbContext zhContext;

        public ModulesController(ZhPracticeDbContext zhContext)
        {
            this.zhContext = zhContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return zhContext.Module != null ? View(await zhContext.Module.ToListAsync()) : Problem("A zh context üres");
        }

        [HttpGet]
        public IActionResult Create() 
        {
            // Create new module class
            Module module = new Module();

            // Create Module ViewModel
            ModuleViewModel moduleViewModel = new ModuleViewModel
            {
                Modules = module,
            };

            return View(moduleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModuleViewModel addModulRequest)
        {
            if(ModelState.IsValid)
            {
                // Check if a module with the same name already exists
                bool moduleExists = await zhContext.Module.AnyAsync(m => m.Name == addModulRequest.Modules.Name);

                if (!moduleExists)
                {

                    // No duplicate so add the new module
                    await zhContext.Module.AddAsync(addModulRequest.Modules);
                    await zhContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else 
                {
                    ViewData["HasDuplicateError"] = true;
                }
            }

            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model) 
        {
            var module = await zhContext.Module.FindAsync(model.Module_Id);
            
            if (module != null) 
            {
                module.Name = model.Name;
                module.Description = model.Description;

                await zhContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        // Delete Module
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostDelete(int id) 
        {
            var module = await zhContext.Module.FindAsync(id);

            if (module != null)
            {
                zhContext.Module.Remove(module);

                await zhContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
