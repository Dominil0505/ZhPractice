using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZhPractice.Models.Answer
{
    public class Answer
    {
        [Key]
        public int Answer_Id{ get; set; }

        [Required]
        public string Text{ get; set; }

        [ForeignKey("Question_Id")]
        public int Question_Id{ get; set; }

    }
}
