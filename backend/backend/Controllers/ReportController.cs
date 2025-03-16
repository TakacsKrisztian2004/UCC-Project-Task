using backend.Models;
using backend.Models.Dtos;
using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportInterface _ReportService;

        public ReportController(IReportInterface ReportService)
        {
            _ReportService = ReportService;
        }

        [HttpGet("getreports")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetAllReports()
        {
            var Reports = await _ReportService.GetAll();
            return Ok(Reports);
        }

        [HttpGet("getresolvedreports")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetByResolved()
        {
            var Reports = await _ReportService.GetResolved();

            if (Reports == null)
            {
                return NotFound();
            }

            return Ok(Reports);
        }

        [HttpGet("getunresolvedreports")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetByUnresolved()
        {
            var Reports = await _ReportService.GetUnresolved();

            if (Reports == null)
            {
                return NotFound();
            }

            return Ok(Reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDto>> GetReportById(Guid id)
        {
            var Report = await _ReportService.GetById(id);

            if (Report == null)
            {
                return NotFound();
            }

            return Ok(Report);
        }

        [HttpPost]
        public async Task<ActionResult<ReportDto>> CreateReport(CreateReportDto createReportDto)
        {
            var Report = await _ReportService.Post(createReportDto);
            return CreatedAtAction(nameof(GetReportById), new { id = Report.Id }, Report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(Guid id, ModifyReportDto modifyReportDto)
        {
            var updatedReport = await _ReportService.Put(id, modifyReportDto);

            if (updatedReport == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("toggleresolved/{id}")]
        public async Task<IActionResult> ToggleReport(Guid id)
        {
            var updatedReport = await _ReportService.Put(id);

            if (updatedReport == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Report>> DeleteReport(Guid id)
        {
            var deletedReport = await _ReportService.DeleteById(id);

            if (deletedReport == null)
            {
                return NotFound();
            }

            return deletedReport;
        }
    }
}