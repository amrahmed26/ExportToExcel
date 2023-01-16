using Aelgak_WebApp.Services;
using ExportToExcel.Models;
using ExportToExcel.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExportToExcel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly ExportToExcelService<Employee> exportToExcelService;
        public EmployeesController(IEmployeeService employeeService )
        {
            this.employeeService = employeeService;
            exportToExcelService = ExportToExcelService<Employee>.GetInstance().Result;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        
        public async Task<IEnumerable<EmployeeViewModel>> GetEmployees() =>
          await  employeeService.GetEmployees();
        

        // GET api/<EmployeesController>/5
        [HttpGet("{EmployeeId}")]
        public async Task<ActionResult> GetEmployee ([FromRoute]int EmployeeId)
        {
            var Employee=await employeeService.GetEmployee(EmployeeId);
            return Employee!=null?Ok(Employee):NotFound();
        }
        [HttpPost("ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            string reportname = $"Users{DateTime.Now}.xlsx";
            var list = await employeeService.GetEmployees();
            if (list.Count > 0)
            {
                var exportbytes = await exportToExcelService.ExporttoExcel(list, reportname);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
            }
            else
            {
                return BadRequest();
            }
        }
        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult> PostEmployee([FromBody] Employee employee)
        {
            var res=await employeeService.PostEmployee(employee);
            return res ? Created(nameof(PostEmployee), employee):BadRequest();
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{EmployeeId}")]
        public async Task<ActionResult> PutEmployee ([FromRoute]int EmployeeId, [FromBody] Employee employee)
        {
            var res=await employeeService.PutEmployee(EmployeeId, employee);
            return res ? NoContent() : BadRequest();
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{EmployeeId}")]
        public async Task<ActionResult> DeleteEmployee([FromRoute] int EmployeeId)
        {
            var res=await employeeService.DeleteEmployee(EmployeeId);
            return res ? Ok() : NotFound();
        }
    }
}
