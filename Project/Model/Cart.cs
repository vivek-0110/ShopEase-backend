﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int ProductId { get; set; }

     //[ForeignKey("ProductId")]
       // public Product Product { get; set; }

        public int UserId { get; set; }

       //[ForeignKey("UserId")]
       // public User User { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }


    }
}
