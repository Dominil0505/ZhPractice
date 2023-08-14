using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using ZhPractice.Data;
using ZhPractice.Models.Question.Question;
using ZhPractice.Areas.Identity;
using ZhPractice.Models;

namespace ZhPractice.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ZhPracticeDbContext zhContext;

        public QuestionsController(ZhPracticeDbContext zhContext)
        {
            this.zhContext = zhContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        { 
            return zhContext.Question != null ? View(await zhContext.Question.ToListAsync()) : Problem("A zh context üres");
        }

        [HttpGet]
        public IActionResult Create() 
        {
            // Create empty question class
            Question question = new Question();

            // Create Questionviewmodel 
            QuestionViewModel questionViewModel = new QuestionViewModel
            {
                Questions = question,
                Modules = zhContext.Module.ToList()
            };

            return View(questionViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(QuestionViewModel addQuestionRequest) 
        {
            if (ModelState.IsValid)
            {

                //Save question to database
                await zhContext.Question.AddAsync(addQuestionRequest.Questions);
                await zhContext.SaveChangesAsync();


                //Save Question - Module relationship
                foreach(int moduleId in addQuestionRequest.SelectedModuleId) 
                {
                    Question_Module questionModule = new Question_Module
                    {
                        Question_Id = addQuestionRequest.Questions.Question_Id,
                        Module_Id = moduleId
                    };
                    await zhContext.Question_Module.AddAsync(questionModule);
                }

                await zhContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            addQuestionRequest.Modules = zhContext.Module.ToList();
            return View("Create");
        }

        [HttpGet]
        /*
         * public async Task<IActionResult> Edit(Guid id) 
        {
            var question = await zhContext.Question.FirstOrDefaultAsync(x => x.Id == id);

            if(question != null) 
            {
                var updateView = new EditViewModel
                {
                    Id = question.Id,
                    Name = question.Name,
                    
                };

                return await Task.Run(() => View("Edit", updateView));
            }
            
            return View();
            
        }*/

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var question = await zhContext.Question.FindAsync(model.Id);

            if (question != null)
            {
                
                question.Name = model.Name;
                

                await zhContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var question = await zhContext.Question.FindAsync(id);

            if(question != null) 
            {
                zhContext.Question.Remove(question);
                await zhContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        
        }
    }
}
