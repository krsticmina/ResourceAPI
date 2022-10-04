using Microsoft.AspNetCore.Mvc;

namespace StaffServiceAPI.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        public int CurrentEmployeeId { get; set; }
        public bool IsAdmin { get; set; }

    }
}
