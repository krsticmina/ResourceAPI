using AutoMapper;
using StaffServiceCore.Models;
using StaffServiceDAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceBLL
{
    public class EmployeeStaffService : IEmployeeStaffService
    {
        private readonly IAdminStaffRepository repository;
        private readonly IMapper mapper;

        public EmployeeStaffService(IAdminStaffRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null) return null;

            return mapper.Map<EmployeeDto>(employee);
        }
    }
}
