using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZhPractice.Models
{
    public class Question_Module
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Question_Id")]
        public int Question_Id { get; set; }

        [ForeignKey("Module_Id")]
        public int Module_Id { get; set; }
    }
}
