using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionInterface _connection;

        public ConnectionController(IConnectionInterface connection)
        {
            _connection = connection;
        }

        [HttpGet("allreportdetails")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllReportDetails()
        {
            object result = await _connection.GetAllReportDetails();

            if (result != null)
            {
                return StatusCode(200, result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("allresolvedreportdetails")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllResolvedReportDetails()
        {
            object result = await _connection.GetAllResolvedReportDetails();

            if (result != null)
            {
                return StatusCode(200, result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("allunresolvedreportdetails")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUnresolvedReportDetails()
        {
            object result = await _connection.GetAllUnresolvedReportDetails();

            if (result != null)
            {
                return StatusCode(200, result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("reportdetailsbyid/{id}")]
        public async Task<ActionResult> GetReportDetailsById(Guid id)
        {
            object result = _connection.GetReportDetailsById(id).Result;

            if (result != null)
            {
                return StatusCode(200, result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("alluserdetails")]
        public async Task<ActionResult> GetAllUserDetails()
        {
            var result = await _connection.GetAllUserDetails();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No user details found.");
            }
        }

        [HttpGet("userdetailsbyusername/{name}")]
        public async Task<ActionResult> GetUserDetailsById(string name)
        {
            var result = await _connection.GetUserDetailsByUsername(name);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound($"User with ID {name} not found.");
            }
        }
        [HttpGet("resolveduserdetailsbyusername/{name}")]
        public async Task<ActionResult> GetResolvedUserDetailsByUsername(string name)
        {
            var result = await _connection.GetResolvedUserDetailsByUsername(name);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound($"User with ID {name} not found.");
            }
        }
        [HttpGet("unresolveduserdetailsbyusername/{name}")]
        public async Task<ActionResult> GetUnresolvedUserDetailsByUsername(string name)
        {
            var result = await _connection.GetUnresolvedUserDetailsByUsername(name);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound($"User with ID {name} not found.");
            }
        }
    }
}