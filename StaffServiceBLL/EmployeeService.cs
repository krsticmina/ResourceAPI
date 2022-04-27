using AutoMapper;
using StaffServiceBLL.Exceptions;
using StaffServiceBLL.Models;
using StaffServiceDAL.Entities;
using StaffServiceDAL.Services;
using static StaffServiceCommon.Enumerations;

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

        private async Task CheckManager(int managerId) 
        {
            var manager = await repository.GetEmployeeByIdAsync(managerId);

            if (manager == null)
            {
                throw new EmployeeNotFoundException($"Manager with Id {managerId} could not be found");
            }
            if (manager.Position!= Position.Manager.ToString())
            {
                throw new NotManagerException("Requested manager does not have managerial position!");
            }
        }

        ///<inheritdoc/>
        public async Task<EmployeeModel?> AddEmployeeAsync(EmployeeForInsertionModel employeeToAdd)
        {
            if ( employeeToAdd.ManagerId!=null)
            {
                if (employeeToAdd.ManagerId == 0)
                {
                    throw new ArgumentOutOfRangeException("There is not an employee with Id 0.");
                }
                await CheckManager((int)employeeToAdd.ManagerId);
            }

            var employee = mapper.Map<Employee>(employeeToAdd);

            await repository.AddEmployeeAsync(employee);

            await repository.SaveChangesAsync();

            var employeeToReturn = mapper.Map<EmployeeModel>(employee);

            return employeeToReturn;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync()
        {

            var employees = await repository.GetAllEmployeesAsync();
            
            return mapper.Map<IEnumerable<EmployeeModel>>(employees);
        }

        ///<inheritdoc/>
        public async Task<EmployeeModel?> GetEmployeeByIdAsync(int employeeId)
        {

            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                throw new EmployeeNotFoundException($"Employee with Id {employeeId} could not be found.");
            }

            return mapper.Map<EmployeeModel>(employee);
        }

        ///<inheritdoc/>
        public async Task UpdateEmployeeAsync(int employeeId, EmployeeForUpdateModel employeeToUpdate)
        {
            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null) 
            {
                throw new EmployeeNotFoundException($"Employee with Id {employeeId} could not be found");
            }
            if (employeeToUpdate.ManagerId != null) 
            {
                if (employee.ManagerId == 0)
                {
                    throw new ArgumentOutOfRangeException("Employee Id cannot be 0.");
                }
                await CheckManager((int)employeeToUpdate.ManagerId);
            }
            else
            {
                employeeToUpdate.ManagerId = employee.ManagerId;
            }

            mapper.Map(employeeToUpdate, employee);

            await repository.SaveChangesAsync();

        }


    }


}