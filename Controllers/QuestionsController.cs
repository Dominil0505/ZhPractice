using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using ZhPractice.Data;
using ZhPractice.Models.Question.Question;
using ZhPractice.Areas.Identity;
using ZhPractice.Models;
using ZhPractice.Models.Answer;

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
            // itt false lesz
            if (ModelState.IsValid)
            {

                // Add new Question
                Question newQuestion = new Question
                {
                    Name = addQuestionRequest.Questions.Name,
                    CreatedDate = DateTime.Now
                };

                zhContext.Question.Add(newQuestion);
                await zhContext.SaveChangesAsync();

                // Save answers
                foreach (var key in Request.Form.Keys)
                {
                    if (key.StartsWith("Answer_"))
                    {
                        var answerText = Request.Form[key].ToString();

                        Answer newAnswer = new Answer
                        {
                            Text = answerText,
                            Question_Id = newQuestion.Question_Id
                        };

                        await zhContext.AddAsync(newAnswer);
                    }
                }

                var selectedModule = await zhContext.Module.FindAsync(addQuestionRequest.SelectedModuleId);
                if (selectedModule != null) 
                {
                    var questionModule = new Question_Module
                    {
                        Question_Id = newQuestion.Question_Id,
                        Module_Id = selectedModule.Module_Id,
                    };

                    zhContext.Question_Module.Add(questionModule);
                }

                await zhContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
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
