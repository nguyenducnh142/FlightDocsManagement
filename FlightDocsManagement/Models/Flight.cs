namespace FlightDocsManagement.Models
{
    public class Flight
    {
        public string FlightId { get; set; }
        public string PlaneId { get; set; } 
        public DateTime Date { get; set; }
        public string PointOfLoading { get; set; }
        public string PointOfUploading { get; set; }
    }
}
