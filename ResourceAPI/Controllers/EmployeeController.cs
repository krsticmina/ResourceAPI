using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ResourceAPI.Entities;
using ResourceAPI.Models;
using ResourceAPI.Services;

namespace ResourceAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public EmployeeController(IRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        /// <summary>
        /// Action for finding an employee using id.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        
        [HttpGet("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var employee = await repository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<EmployeeDto>(employee));
        }


        /// <summary>
        /// Action for retrieving all employees from database
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {

            var employees = await repository.GetAllEmployeesAsync();

            return Ok(mapper.Map<IEnumerable<EmployeeDto>>(employees));

        }

        /// <summary>
        /// Action for adding employee to database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
       
        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] EmployeeForInsertionDto employee)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (employee.ManagerId!=0)
            {
                var manager = await repository.GetEmployeeByIdAsync(employee.ManagerId);

                if (manager == null)
                {
                    return BadRequest($"Manager with id {employee.ManagerId} does not exist.");
                }
            }

            var employeeToAdd = mapper.Map<Employee>(employee);

            await repository.AddEmployeeAsync(employeeToAdd);

            await repository.SaveChangesAsync();

            var employeeToReturn = mapper.Map<EmployeeDto>(employeeToAdd);

            return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = employeeToReturn.Id }, employeeToReturn);

        }

        /// <summary>
        ///  Action for partially updating employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        
        [HttpPatch("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PartiallyUpdateEmployee(int employeeId, [FromBody] JsonPatchDocument<EmployeeForInsertionDto> patchDocument)
        {
            var employee = await repository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeToUpdate = mapper.Map<EmployeeForInsertionDto>(employee);

            patchDocument.ApplyTo(employeeToUpdate, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(employeeToUpdate))
            {
                return BadRequest(ModelState);
            }

            mapper.Map(employeeToUpdate, employee);

            repository.UpdateEmployee(employee);

            await repository.SaveChangesAsync();

            return NoContent();

        }

        /// <summary>
        /// Action for updating employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeToUpdate"></param>
        /// <returns></returns>
        
        [HttpPut("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEmployee(int employeeId, EmployeeForInsertionDto employeeToUpdate)
        {
            var employee = await repository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            mapper.Map(employeeToUpdate, employee);

            repository.UpdateEmployee(employee);

            await repository.SaveChangesAsync();

            return NoContent();
        }


    }
}
