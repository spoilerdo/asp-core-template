using System.ComponentModel.DataAnnotations;

namespace Back_End.Persistence.Models
{
    public class TemplateDTO
    {
        [Required]
        public string Name { get; set; }
        public object Data { get; set; }
    }
}