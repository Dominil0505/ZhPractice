using System.ComponentModel.DataAnnotations;

namespace ZhPractice.Models.Module
{
    public class Module
    {
        [Required]
        [Key]
        public int Module_Id{ get; set; }

        [Required]
        public string Name{ get; set; }

        public string Description { get; set; }
    }
}
