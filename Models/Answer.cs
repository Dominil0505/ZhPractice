using System.ComponentModel.DataAnnotations.Schema;

namespace ZhPractice.Models
{
    public class Answer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Question_Id{ get; set; }
    }
}
