using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazingoBooks.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        public string ImageURL { get; set; }

        [Required]
        public int CategoryId { get; set; }

        //[ForeignKey("CategoryId")]
        // Not necessary as the foreign key will be created automatically by the Entity Framework 
        public Category Category { get; set; }

        [Required]
        public int CoverTypeId { get; set; }

        //[ForeignKey("CoverTypeId")]
        public CoverType CoverType { get; set; }
    }
}
