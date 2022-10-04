using StaffServiceBLL.Models;

namespace StaffServiceBLL
{
    public interface IEmployeeService
    {

        /// <summary>
        /// Method for getting an employee by Id.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <exception cref="Exceptions.EmployeeNotFoundException">Thrown when an employee could not be found in the database.</exception>
        /// <returns></returns>
        Task<EmployeeModel?> GetEmployeeByIdAsync(int employeeId, int currentEmployeeId, bool isAdmin, bool isEmployee);

        /// <summary>
        /// Method for getting all employees.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync(int currentEmployeeId, bool isAdmin);

        /// <summary>
        /// Method for adding an employee.
        /// </summary>
        /// <param name="employeeToAdd"></param>
        /// <exception cref="Exceptions.EmployeeNotFoundException">Thrown when an employee could not be found in the database.</exception>
        /// <exception cref="Exceptions.NotManagerException">Thrown when the requested manager does not have managerial position.</exception>
        /// <returns></returns>
        Task<EmployeeModel?> AddEmployeeAsync(EmployeeForInsertionModel employeeToAdd, int currentEmployeeId, bool isAdmin);

        /// <summary>
        /// Method for updating an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeToUpdate"></param>
        /// <exception cref="Exceptions.EmployeeNotFoundException">Thrown when an employee could not be found in the database.</exception>
        /// <exception cref="Exceptions.NotManagerException">Thrown when the requested manager does not have managerial position.</exception>
        /// <returns></returns>
        Task UpdateEmployeeAsync(int employeeId, EmployeeForUpdateModel employeeToUpdate, int currentEmployeeId, bool isAdmin);


        /// <summary>
        /// Method that checks if a user is an employee.
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="Exceptions.EmployeeNotFoundException">Thrown when an employee could not be found in the database.</exception>
        /// <returns></returns>
        Task<EmployeeModel> FindUserInDatabaseAsync(int userId);

    }
}
