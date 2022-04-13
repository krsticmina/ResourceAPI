using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffServiceBLL;

namespace StaffServiceAPI.Controllers
{
    [Route("api/{employeeId}")]
    [ApiController]
    public class EmployeeStaffController : ControllerBase
    {
        private readonly IEmployeeStaffService service;

        public EmployeeStaffController(IEmployeeStaffService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Action for finding an employee using Id.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await service.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return NotFound($"Employee with Id {employeeId} not found.");
            }

            return Ok(employee);
        }

    }
}
