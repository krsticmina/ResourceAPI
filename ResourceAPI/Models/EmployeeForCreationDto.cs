﻿namespace ResourceAPI.Models
{
    public class EmployeeForCreationDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
