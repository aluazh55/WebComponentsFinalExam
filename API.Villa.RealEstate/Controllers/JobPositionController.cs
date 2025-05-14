using API.Villa.RealEstate.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Villa.RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class JobPositionController : ControllerBase
    {
        private readonly AppDbContext _db;

        public JobPositionController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("getAllPositions")]
        public async Task<ActionResult<List<JobPosition>>> GetAllPositions()
        {
            var positions = await _db.JobPositions.ToListAsync();
            return Ok(positions);
        }

        [HttpGet("getPositionById/{id:int}")]
        public async Task<ActionResult<JobPosition>> GetPositionById(int id)
        {
            var position = await _db.JobPositions.FirstOrDefaultAsync(p => p.Id == id);
            if (position == null)
                return NotFound(new { message = "Position not found!" });

            return Ok(position);
        }

        [HttpPost("createPosition")]
        public async Task<IActionResult> CreatePosition([FromBody] JobPosition position)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _db.JobPositions.AddAsync(position);
                await _db.SaveChangesAsync();
                return Ok(new { message = "The position successfully added!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("deletePosition/{id:int}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            try
            {
                var data = await _db.JobPositions.FirstOrDefaultAsync(p => p.Id == id);
                if (data == null)
                    return NotFound(new { message = "Position not found!" });

                _db.JobPositions.Remove(data);
                await _db.SaveChangesAsync();
                return Ok(new { message = "The position successfully deleted!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
