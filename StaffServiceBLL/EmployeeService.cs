using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using StaffServiceDAL.Entities;
using StaffServiceDAL.Services;
using StaffServiceCore.Models;

namespace StaffServiceBLL
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

       
        /// <summary>
        /// Method for adding an employee.
        /// </summary>
        /// <param name="employeeToAdd"></param>
        /// <returns></returns>
        public async Task<EmployeeDto?> AddEmployeeAsync(EmployeeForInsertionDto employeeToAdd)
        {
            
            if (employeeToAdd.ManagerId != 0 && employeeToAdd.ManagerId!=null)
            {
                var manager = await repository.GetEmployeeByIdAsync((int)employeeToAdd.ManagerId);

                if (manager == null || manager.Position != "Manager")
                {
                    return null;
                }
            }

            var employee = mapper.Map<Employee>(employeeToAdd);

            await repository.AddEmployeeAsync(employee);

            await repository.SaveChangesAsync();

            var employeeToReturn = mapper.Map<EmployeeDto>(employee);

            return employeeToReturn;
        }


        /// <summary>
        /// Method for getting all employees.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {

            var employees = await repository.GetAllEmployeesAsync();
            
            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }


        
        /// <summary>
        /// Method for getting an employee by Id.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int employeeId)
        {

            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return null;
            }

            return mapper.Map<EmployeeDto>(employee);
        }


        /// <summary>
        /// Method for updating an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeToUpdate"></param>
        /// <returns></returns>
        public async Task<EmployeeDto?> UpdateEmployeeAsync(int employeeId, EmployeeForUpdateDto employeeToUpdate)
        {
            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null) 
            {
                return null; 
            }

            if (employeeToUpdate.ManagerId == null)
            {
                employeeToUpdate.ManagerId = employee.ManagerId;
            }

            employeeToUpdate.ModifiedAt = DateTime.UtcNow;

            mapper.Map(employeeToUpdate, employee);

            await repository.SaveChangesAsync();

            return mapper.Map<EmployeeDto>(employee);

        }


        /// <summary>
        /// Method for getting an employee for partial update.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<EmployeeForUpdateDto?> GetEmployeeForUpdate(int employeeId)
        {
            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return null;
            }
            return mapper.Map<EmployeeForUpdateDto>(employee);
        }

       
    }


}