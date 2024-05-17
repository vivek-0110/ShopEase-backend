﻿using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class ProductDto
    {
        
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter product name")]

        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please enter Description")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter Price")]


        public int Price { get; set; }
        [Required(ErrorMessage = "Please enter Stock")]

        public int Stock { get; set; }
        [Required(ErrorMessage = "Please enter CategoryId")]

        public int CategoryId { get; set; }


        public enum StatusEnum
        {
            Active,
            Inactive
        }

        public StatusEnum Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

    }
}
