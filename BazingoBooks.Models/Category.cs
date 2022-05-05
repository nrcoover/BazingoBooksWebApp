using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BazingoBooks.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage ="Display Order must be greater than 1, but cannot exceed 100.")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDataTime { get; set; } = DateTime.Now;
    }
}
