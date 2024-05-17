using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class CartDto
    {
        [Key]
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

    }
}
