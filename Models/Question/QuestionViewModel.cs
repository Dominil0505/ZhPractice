namespace ZhPractice.Models.Question.Question
{
    using ZhPractice.Models.Module;
    using ZhPractice.Models.Answer;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class QuestionViewModel
    {
        public Question Questions { get; set; }
        public List<Module> Modules { get; set; }
        public List<Answer> Answers { get; set; }
        public List<SelectListItem> ModuleSelectList{ get; set; }
        public string SelectedModule{ get; set; }

    }
}
