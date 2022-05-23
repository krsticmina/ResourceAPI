using AutoMapper;
using StaffServiceBLL.Exceptions;
using StaffServiceBLL.Models;
using StaffServiceCommon;
using StaffServiceDAL.Entities;
using StaffServiceDAL.Services;

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

        private async Task CheckManager(int managerId, int currentEmployeeId, bool isAdmin) 
        {
            var manager = await repository.GetEmployeeByIdAsync(managerId);

            if (manager == null)
            {
                throw new EmployeeNotFoundException($"Manager with Id {managerId} could not be found");
            }
            if (!isAdmin && managerId!=currentEmployeeId) 
            {
                await CheckIfManagerIsAuthorizedAsync(managerId, currentEmployeeId);
            }
            if (manager.Position!= Position.Manager.ToString())
            {
                throw new NotManagerException("Requested manager does not have managerial position!");
            }
        }

        ///<inheritdoc/>
        public async Task<EmployeeModel?> AddEmployeeAsync(EmployeeForInsertionModel employeeToAdd, int currentEmployeeId, bool isAdmin)
        {
            if (!isAdmin && employeeToAdd.ManagerId==null)
            {
                throw new UnauthorizedException("Managers can only add employees for themselves or for their subordinates.");
            }
            if (employeeToAdd.ManagerId!=null)
            {
                await CheckManager((int)employeeToAdd.ManagerId, currentEmployeeId, isAdmin);
            }

            var employee = mapper.Map<Employee>(employeeToAdd);

            await repository.AddEmployeeAsync(employee);

            await repository.SaveChangesAsync();

            var employeeToReturn = mapper.Map<EmployeeModel>(employee);

            return employeeToReturn;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync(int currentEmployeeId, bool isAdmin)
        {
            if (!isAdmin) 
            { 
                var employeesForManager = await repository.GetAllEmployeesForManagerAsync(currentEmployeeId);

                return mapper.Map<IEnumerable<EmployeeModel>>(employeesForManager);
            }

            var employees = await repository.GetAllEmployeesAsync();
            
            return mapper.Map<IEnumerable<EmployeeModel>>(employees);
        }

        ///<inheritdoc/>
        public async Task<EmployeeModel?> GetEmployeeByIdAsync(int employeeId, int currentEmployeeId, bool isAdmin, bool isEmployee)
        {

            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                throw new EmployeeNotFoundException($"Employee with Id {employeeId} could not be found.");
            }
            if (!isAdmin && employeeId != currentEmployeeId)
            {
                if (!isEmployee)
                {
                    await CheckIfManagerIsAuthorizedAsync(employeeId, currentEmployeeId);
                }
                else
                {
                    throw new UnauthorizedException($"You don't have access to employee with Id {employeeId}");
                }
            }
            return mapper.Map<EmployeeModel>(employee);
        }

        ///<inheritdoc/>
        public async Task UpdateEmployeeAsync(int employeeId, EmployeeForUpdateModel employeeToUpdate, int currentEmployeeId, bool isAdmin)
        {

            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null) 
            {
                throw new EmployeeNotFoundException($"Employee with Id {employeeId} could not be found");
            }

            if (!isAdmin)
            {
                await CheckIfManagerIsAuthorizedAsync(employee.Id, currentEmployeeId);

            }
            if (employeeToUpdate.ManagerId != null)
            {
                await CheckManager((int)employeeToUpdate.ManagerId, currentEmployeeId, isAdmin);
            }
            else
            {
                employeeToUpdate.ManagerId = employee.ManagerId;
            }

            mapper.Map(employeeToUpdate, employee);

            await repository.SaveChangesAsync();

        }

        private async Task CheckIfManagerIsAuthorizedAsync(int employeeId, int managerId) 
        {
            var employee = await repository.CheckIfManagerIsAuthorizedAsync(employeeId, managerId);

            if (employee == null)
            {
                throw new UnauthorizedException($"You don't have access to employee with Id {employeeId}.");
            }
        }

        ///<inheritdoc/>
        public async Task<EmployeeModel> FindUserInDatabaseAsync(int userId) 
        {
            var employee = await repository.FindUserInDatabaseAsync(userId);

            if (employee == null)
            {
                throw new EmployeeNotFoundException($"Employee with User Id {userId} could not be found.");
            }

            return mapper.Map<EmployeeModel>(employee);
        }
    }


}