using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployeeById(int id) 
        {
            var employee = await repository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<EmployeeDto>(employee));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var employees = await repository.GetAllEmployeesAsync();

            return Ok(mapper.Map<IEnumerable<EmployeeDto>>(employees));

        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] EmployeeForCreationDto employee) 
        {
            var employeeToAdd = mapper.Map<Employee>(employee);

            await repository.AddEmployeeAsync(employeeToAdd);

            await repository.SaveChangesAsync();

            var employeeToReturn = mapper.Map<EmployeeDto>(employeeToAdd);

            return Ok();
        //    return CreatedAtRoute("GetEmployee", new { employeeId = employeeToReturn.Id }, employeeToReturn);
        
        }
    }
}
