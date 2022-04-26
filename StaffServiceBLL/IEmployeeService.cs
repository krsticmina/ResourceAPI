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
        Task<EmployeeModel?> GetEmployeeByIdAsync(int employeeId);

        /// <summary>
        /// Method for getting all employees.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync();

        /// <summary>
        /// Method for adding an employee.
        /// </summary>
        /// <param name="employeeToAdd"></param>
        /// <exception cref="Exceptions.EmployeeNotFoundException">Thrown when an employee could not be found in the database.</exception>
        /// <exception cref="Exceptions.NotManagerException">Thrown when the requested manager does not have managerial position.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the employee Id is out of specified range"</exception>
        /// <returns></returns>
        Task<EmployeeModel?> AddEmployeeAsync(EmployeeForInsertionModel employeeToAdd);

        /// <summary>
        /// Method for updating an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeToUpdate"></param>
        /// <exception cref="Exceptions.EmployeeNotFoundException">Thrown when an employee could not be found in the database.</exception>
        /// <exception cref="Exceptions.NotManagerException">Thrown when the requested manager does not have managerial position.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the employee Id is out of specified range"</exception>
        /// <returns></returns>
        Task UpdateEmployeeAsync(int employeeId, EmployeeForUpdateModel employeeToUpdate);
    
    }
}
