﻿using System.ComponentModel.DataAnnotations;
using static StaffServiceBLL.Models.Enumerations;

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
