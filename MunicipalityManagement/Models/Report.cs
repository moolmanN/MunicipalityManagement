namespace MunicipalityManagement.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        public int CitizenID { get; set; }
        public Citizen? Citizen { get; set; }  // Ensure Citizen is available in the same namespace
        public string ReportType { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Under Review";
    }
}
