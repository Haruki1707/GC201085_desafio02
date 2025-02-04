﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }
    }
}
