
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StaffServiceBLL;
using StaffServiceCore.Models;

namespace StaffServiceAPI.Controllers
{
    [Route("api/manager/{managerId}/employees")]
    [ApiController]
    public class ManagerStaffController : ControllerBase
    {
        private readonly IManagerStaffService _service;

        public ManagerStaffController(IManagerStaffService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Action for finding an employee using id.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>

        [HttpGet("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeByIdAsync(int managerId, int employeeId)
        {
            var employee = await _service.GetEmployeeByIdAsync(managerId, employeeId);

            if (employee == null)
            {
                return NotFound($"Employee with Id {employeeId} not found or could not be accessed.");
            }
            return Ok(employee);
        }


        /// <summary>
        /// Action for retrieving all employees from database 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync(int managerId)
        {
            var employees = await _service.GetAllEmployeesAsync(managerId);

            return Ok(employees);

        }

        /// <summary>
        /// Action for adding an employee to the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeForInsertionDto employee)
        {
            var res = await _service.AddEmployeeAsync(employee);

            if (res == null)
            {
                return BadRequest($"Manager with Id {employee.ManagerId} doesn't exist or does not have managerial position.");
            }

            return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { managerId = res.managerId, employeeId = res.Id }, res);

        }


        /// <summary>
        /// Action for updating an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeToUpdate"></param>
        /// <returns></returns>

        [HttpPut("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEmployeeAsync(int employeeId, int managerId, EmployeeForUpdateDto employeeToUpdate)
        {
            var res = await _service.UpdateEmployeeAsync(employeeId, managerId, employeeToUpdate);

            if (res == null) return NotFound($"Employee with Id {employeeId} not found or could not be accessed.");

            return NoContent();
        }


        /// <summary>
        ///  Action for partially updating an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>

        [HttpPatch("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PartiallyUpdateEmployeeAsync(int employeeId, int managerId, [FromBody] JsonPatchDocument patchDocument)
        {
            var employeeToUpdate = await _service.GetEmployeeForUpdateAsync(managerId, employeeId);

            if (employeeToUpdate == null) return NotFound($"Employee with Id {employeeId} not found or could not be accessed.");

            patchDocument.ApplyTo(employeeToUpdate);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(employeeToUpdate))
            {
                return BadRequest();
            }

            var res = await _service.UpdateEmployeeAsync(employeeId, managerId, employeeToUpdate);

            if (res == null) return BadRequest();

            return NoContent();

        }


    }
}
