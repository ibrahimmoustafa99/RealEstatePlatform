using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstatePlatform_API.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }

    }
}
