using API.Villa.RealEstate.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Villa.RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EmployeeController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("getEmployees")]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await _db.Employees
                .Include(e => e.JobPosition)
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("getEmployeeById/{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _db.Employees
                .Include(e => e.JobPosition)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound(new { message = "Employee not found!" });

            return Ok(employee);
        }

        [HttpPost("createEmployee")]
        public async Task<IActionResult> CreateEmployee([FromForm] Employee employee, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    using var memory = new MemoryStream();
                    await file.CopyToAsync(memory);
                    employee.Image = memory.ToArray(); // поле типа byte[] в Employee
                }

               

                await _db.Employees.AddAsync(employee);
                await _db.SaveChangesAsync();

                return Ok(new { message = "The employee successfully added!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpDelete("deleteEmployee/{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null)
                    return NotFound(new { message = "Employee not found!" });

                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();

                return Ok(new { message = "The employee successfully deleted!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
