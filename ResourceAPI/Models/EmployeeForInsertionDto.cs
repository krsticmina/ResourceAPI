using System.ComponentModel.DataAnnotations;

namespace ResourceAPI.Models
{
    public class EmployeeForInsertionDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required(ErrorMessage = "You should provide an email value.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a phone number value.")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; } = string.Empty;
        public int ManagerId { get; set; }

        [Required(ErrorMessage = "You should provide a salary value.")]
        public int Salary { get; set; }
    }
}
