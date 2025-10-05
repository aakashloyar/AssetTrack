namespace AssetApp.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }       
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Designation { get; set; }
        public EmployeeStatus Status { get; set; }  
    }
}
