using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StaffServiceAPI.Models;
using StaffServiceBLL;
using StaffServiceBLL.Models;

namespace StaffServiceAPI.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService service;
    private readonly IMapper mapper;

    public EmployeeController(IEmployeeService service, IMapper mapper)
    {
        this.service = service ?? throw new ArgumentNullException(nameof(service));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    /// <summary>
    /// Action for finding an employee using Id.
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    [HttpGet("{employeeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEmployeeByIdAsync(int employeeId)
    {
        var employee = await service.GetEmployeeByIdAsync(employeeId);

        return Ok(mapper.Map<EmployeeDto>(employee));
    }


    /// <summary>
    /// Action for retrieving all employees from the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync()
    {
        var employees = await service.GetAllEmployeesAsync();

        return Ok(mapper.Map<IEnumerable<EmployeeDto>>(employees));

    }

    /// <summary>
    /// Action for adding an employee to the database.
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AddEmployeeAsync([FromBody] EmployeeForInsertionDto employee)
    {
        var employeeToAdd = mapper.Map<EmployeeForInsertionModel>(employee);

        var result = await service.AddEmployeeAsync(employeeToAdd);

        return CreatedAtAction(nameof(AddEmployeeAsync),result);

    }


    /// <summary>
    /// Action for updating an employee.
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="employeeToUpdate"></param>
    /// <returns></returns>

    [HttpPut("{employeeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateEmployeeAsync(int employeeId, EmployeeForUpdateDto employeeToUpdate)
    {
        var employee = mapper.Map<EmployeeForUpdateModel>(employeeToUpdate);

        await service.UpdateEmployeeAsync(employeeId, employee);

        return Ok();
    }


    /// <summary>
    ///  Action for partially updating an employee.
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="patchDocument"></param>
    /// <returns></returns>
    [HttpPatch("{employeeId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PartiallyUpdateEmployeeAsync(int employeeId, [FromBody] JsonPatchDocument patchDocument)
    { 
        var employee = await service.GetEmployeeByIdAsync(employeeId);

        var employeeToUpdate = mapper.Map<EmployeeForUpdateDto>(employee);

        patchDocument.ApplyTo(employeeToUpdate);

        if (!TryValidateModel(employeeToUpdate))
        {
            return BadRequest("Invalid input for partially updating employee");
        }

        await service.UpdateEmployeeAsync(employeeId, mapper.Map<EmployeeForUpdateModel>(employeeToUpdate));

        return NoContent();

    }


}
