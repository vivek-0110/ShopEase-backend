using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class CategoryDto
    {
        [Key]
        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; }
    }
}
