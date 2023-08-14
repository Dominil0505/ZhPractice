﻿using Microsoft.AspNetCore.Mvc;
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
                // Save modules

                await zhContext.Module.AddAsync(addModulRequest.Modules);
                await zhContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Create");
        }
    }
}