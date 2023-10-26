namespace FlightDocsManagement.Models
{
    public class Flight
    {
        public string FlightId { get; set; }
        public string PlaneId { get; set; } 
        public DateTime FlightDate { get; set; }
        public string PointOfLoading { get; set; }
        public string PointOfUploading { get; set; }
        public string FlightStatus { get; set; }
        public int FlightTime { get; set; }
    }
}
