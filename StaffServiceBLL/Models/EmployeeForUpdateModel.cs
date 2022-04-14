using System.ComponentModel.DataAnnotations;

namespace StaffServiceBLL.Models
{
    public enum Position { Admin = 1, Manager = 2, Employee = 3}
    public class EmployeeForUpdateModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"(.*)@(.*)\.(.*)", ErrorMessage = "Input email not valid.")]
        public string Email { get; set; } = string.Empty;
        public int? ManagerId { get ; set; }
        [Required]
        [EnumDataType(typeof(Position))]
        public string Position { get; set; } = string.Empty;
        public DateTime ModifiedAt { get; set; } 
    }
}
