using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceAPI.Services;

namespace ResourceAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository repository;

        public EmployeeController(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


    }
}
