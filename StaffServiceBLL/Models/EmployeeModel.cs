using System.ComponentModel.DataAnnotations;

namespace StaffServiceBLL.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? managerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public static EmployeeModel NullEmployee = new() { }; 
    }
}
