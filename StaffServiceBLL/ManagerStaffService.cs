using AutoMapper;
using StaffServiceCore.Models;
using StaffServiceDAL.Entities;
using StaffServiceDAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceBLL
{
    public class ManagerStaffService : IManagerStaffService
    {
        private readonly IManagerStaffRepository _repository;
        private readonly IMapper mapper;

        public ManagerStaffService(IManagerStaffRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Method for getting all employees for a manager.
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(int managerId)
        {

            var employees = await _repository.GetAllEmployeesAsync(managerId);

            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        /// <summary>
        /// Method for getting an employee for a manager using Id.
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int managerId, int employeeId)
        {

            var employee = await _repository.GetEmployeeByIdAsync(managerId, employeeId);

            if (employee == null)
            {
                return null;
            }

            return mapper.Map<EmployeeDto>(employee);


        }

        /// <summary>
        /// Method for updating an employee (for a specific manager).
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="managerId"></param>
        /// <param name="employeeToUpdate"></param>
        /// <returns></returns>
        public async Task<EmployeeDto?> UpdateEmployeeAsync(int employeeId, int managerId, EmployeeForUpdateDto employeeToUpdate)
        {
            var employee = await _repository.GetEmployeeByIdAsync(managerId, employeeId);

            if (employee == null) return null;

            if (employeeToUpdate.ManagerId == null)
            {
                employeeToUpdate.ManagerId = employee.ManagerId;
            }
            employeeToUpdate.ModifiedAt = DateTime.UtcNow;

            mapper.Map(employeeToUpdate, employee);

            await _repository.SaveChangesAsync();

            return mapper.Map<EmployeeDto>(employee);

        }

        /// <summary>
        /// Method for getting an employee for partial update (for a specific manager).
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<EmployeeForUpdateDto?> GetEmployeeForUpdateAsync(int managerId, int employeeId)
        {
            var employee = await _repository.GetEmployeeByIdAsync(managerId, employeeId);

            if (employee == null)
            {
                return null;
            }
            return mapper.Map<EmployeeForUpdateDto>(employee);
        }

        /// <summary>
        /// Method for adding an employee.
        /// </summary>
        /// <param name="employeeToAdd"></param>
        /// <returns></returns>
        public async Task<EmployeeDto?> AddEmployeeAsync(EmployeeForInsertionDto employeeToAdd)
        {

            if (employeeToAdd.ManagerId != 0 && employeeToAdd.ManagerId != null)
            {
                var manager = await _repository.GetEmployeeByIdAsync((int)employeeToAdd.ManagerId);

                if (manager == null || manager.Position != "Manager")
                {
                    return null;
                }
            }

            var employee = mapper.Map<Employee>(employeeToAdd);

            await _repository.AddEmployeeAsync(employee);

            await _repository.SaveChangesAsync();

            var employeeToReturn = mapper.Map<EmployeeDto>(employee);

            return employeeToReturn;
        }

    }

}
