using Microsoft.AspNetCore.Identity;

namespace RealEstatePlatform_API.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Property> Properties { get; set; } // if user is Agent
        public ICollection<Favorite> Favorites { get; set; }  // if user is Buyer
        public ICollection<Booking> Bookings { get; set; }    // if user is Buyer
    }
}
