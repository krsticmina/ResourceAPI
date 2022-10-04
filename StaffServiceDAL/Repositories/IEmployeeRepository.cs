using StaffServiceDAL.Entities;

namespace StaffServiceDAL.Services
{
    public interface IEmployeeRepository
    {

        /// <summary>
        ///  Asynchronous method for retrieving an employee using Id.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        /// <summary>
        /// Asynchronous method for retrieving all employees from database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        /// <summary>
        /// Asynchronous method for adding an employee to database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task AddEmployeeAsync(Employee employee);
        /// <summary>
        /// Asynchronous method for saving changes made to database
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Asynchronous method that gets all employees for a manager.
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetAllEmployeesForManagerAsync(int managerId);

        /// <summary>
        /// Asynchronous method that checks if a manager can access an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        Task<Employee?> CheckIfManagerIsAuthorizedAsync(int employeeId, int managerId);
        /// <summary>
        /// Asynchronous method that finds a user in the employee database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Employee?> FindUserInDatabaseAsync(int userId);

    }
}
