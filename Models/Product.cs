using Rinboku.Infraestructure.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rinboku.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = "The product needs a description"), MinLength(4, ErrorMessage = "Minimum length is 2")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Minimum value is 1")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a category")]
        public long CategoryId { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; } = "no-image.png";

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
