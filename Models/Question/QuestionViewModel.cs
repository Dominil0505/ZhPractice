namespace ZhPractice.Models.Question.Question
{
    using ZhPractice.Models.Module;
    using ZhPractice.Models.Answer;
    public class QuestionViewModel
    {
        public Question Questions { get; set; }
        public List<Module> Modules { get; set; }
        public Answer Answers { get; set; }
        public List<int> SelectedModuleId{ get; set; }

    }
}
