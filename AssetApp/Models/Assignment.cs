namespace AssetApp.Models
{
    public class Assignment
    {
        public int AssignmentID { get; set; }
        public int AssetID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string? Notes { get; set; }
    }
}
