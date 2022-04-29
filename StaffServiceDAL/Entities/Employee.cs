using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffServiceDAL.Entities
{
    public class Employee
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public string Position { get; set; } = String.Empty;
        [ForeignKey("ManagerId")]
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }
        public Employee(string name)
        {
            Name = name;
        }
    }
}
