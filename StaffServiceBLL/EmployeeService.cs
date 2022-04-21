using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using StaffServiceDAL.Entities;
using StaffServiceDAL.Services;
using StaffServiceBLL.Models;
using StaffServiceBLL.Exceptions;

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
        public async Task<EmployeeModel?> AddEmployeeAsync(EmployeeForInsertionModel employeeToAdd)
        {
            
            if (employeeToAdd.ManagerId != 0 && employeeToAdd.ManagerId!=null)
            {
                var manager = await repository.GetEmployeeByIdAsync((int)employeeToAdd.ManagerId);

                if (manager == null)
                {
                    throw new EmployeeNotFoundException($"Manager with Id {employeeToAdd.ManagerId} could not be found");
                }
                if( manager.Position != "Manager")
                {
                    throw new NotManagerException("Requested manager does not have managerial position!");
                }
            }

            var employee = mapper.Map<Employee>(employeeToAdd);

            await repository.AddEmployeeAsync(employee);

            await repository.SaveChangesAsync();

            var employeeToReturn = mapper.Map<EmployeeModel>(employee);

            return employeeToReturn;
        }


        /// <summary>
        /// Method for getting all employees.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync()
        {

            var employees = await repository.GetAllEmployeesAsync();
            
            return mapper.Map<IEnumerable<EmployeeModel>>(employees);
        }


        
        /// <summary>
        /// Method for getting an employee by Id.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<EmployeeModel?> GetEmployeeByIdAsync(int employeeId)
        {

            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return null;
            }

            return mapper.Map<EmployeeModel>(employee);
        }


        /// <summary>
        /// Method for updating an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeToUpdate"></param>
        /// <returns></returns>
        public async Task UpdateEmployeeAsync(int employeeId, EmployeeForUpdateModel employeeToUpdate)
        {
            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null) 
            {
                throw new EmployeeNotFoundException($"Employee with Id {employeeId} could not be found");
            }

            if (employeeToUpdate.ManagerId == null)
            {
                employeeToUpdate.ManagerId = employee.ManagerId;
            }

            mapper.Map(employeeToUpdate, employee);

            await repository.SaveChangesAsync();

        }


    }


}