using System.ComponentModel.DataAnnotations;

namespace ResourceAPI.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public int Salary { get; set; }

    }
}
