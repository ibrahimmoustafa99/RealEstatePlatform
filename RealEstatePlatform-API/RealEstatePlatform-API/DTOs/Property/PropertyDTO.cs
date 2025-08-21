namespace RealEstatePlatform_API.DTOs.Property
{
    public class PropertyDTO
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } // House , Apartment
        public  int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<string> Images { get; set; } = new List<string>();

        public string AgentId { get; set; } 
    }
}
