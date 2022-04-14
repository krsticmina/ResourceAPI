using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceBLL.Models
{
    public class EmployeeForInsertionModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"(.*)@(.*)\.(.*)")]
        public string Email { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
        [Required]
        [EnumDataType(typeof(Position))]
        public string Position { get; set; } = string.Empty;
    }
}
