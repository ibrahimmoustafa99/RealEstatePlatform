using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstatePlatform_API.Models
{
    public class Favorite
    {
        public int Id { get; set; }
         public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }

    }
}
