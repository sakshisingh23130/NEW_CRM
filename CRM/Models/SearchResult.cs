namespace CRM.Models
{
    public class SearchResult
    {
        public string Type { get; set; } // "Customer" or "Employee"
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Extra { get; set; } // Position for employee
    }
}
