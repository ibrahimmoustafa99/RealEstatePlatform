using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstatePlatform_API.Models
{
    public enum PropertyTypes {House, Apartment}
    public class Property
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public PropertyTypes Type { get; set; } // House , Apartment 
        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public bool IsAvailable { get; set; }= true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Forign keys
        [Required]
        public string AgentId { get; set; }
        [ForeignKey("AgentId")]
        public ApplicationUser Agent { get; set; }
        
        // Navigations

        public ICollection<PropertyImage > Images { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public  ICollection<Booking> Bookings { get; set; }



    }
}
