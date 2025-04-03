namespace MunicipalityManagement.Models
{
    public class ServiceRequest
    {
        public int RequestID { get; set; } // This is the Primary Key
        public int CitizenID { get; set; } // This is a Foreign Key
        public Citizen Citizen { get; set; } = new Citizen(); // Navigation Property
        public string ServiceType { get; set; } = string.Empty; // Required
        public DateTime RequestDate { get; set; } = DateTime.Now; // Default Value
        public string Status { get; set; } = "Pending"; // Default Value
    }
}
