using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using experian_example.Dtos;
using experian_example.BusinessLogic;

namespace experian_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeBusinessLogic _businessLogic;

        public EmployeesController(ILogger<EmployeesController> logger, IEmployeeBusinessLogic businessLogic)
        {
            _logger = logger;
            _businessLogic = businessLogic;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            try
            {
                var employee = await _businessLogic.Get(id);
                return Ok(employee);
            }
            catch (Exception e)
            {
                return NoContent();//todo return internal server error
            }
        }

        //employee has been created already
        [HttpPost("employee/{id}")] //todo: use guid or long
        public async Task<ActionResult> Post(int id, [FromBody] ContactDto contact)
        {
            //
            try
            {
                await _businessLogic.CreateContact(id, contact);
                return Created("", null); //todo: populate with resource route and meaning value
            }
            catch (Exception e)
            {
                return NoContent();//todo return internal server error
            }
        }

        [HttpPost] //todo: use guid or long
        public async Task<IActionResult> Post([FromBody] EmployeeDto employeeDto)
        {
            //
            try
            {
                var employeeId = await _businessLogic.CreateEmployee(employeeDto);
                return Created("", employeeId);
            }
            catch(Exception e)
            {
                return NoContent();//todo return internal server error
            }

        }
    }
}
