using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceCore.Models
{
    public class EmployeeForInsertionDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required(ErrorMessage = "You should provide an email value.")]
        [RegularExpression(@"(.*)@(.*)\.(.*)", ErrorMessage = "Input email not valid.")]
        public string Email { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
        [Required(ErrorMessage = "You should provide a role.")]
        [EnumDataType(typeof(Position), ErrorMessage = "Must be Admin, Manager or Employee")]
        public string Position { get; set; } = string.Empty;
    }
}
