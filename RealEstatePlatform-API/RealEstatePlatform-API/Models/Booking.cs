using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstatePlatform_API.Models
{
    public enum BookingStatus { Pending, Approved, Rejected }
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }  // Pending, Approved, Rejected
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
         

    }
}
