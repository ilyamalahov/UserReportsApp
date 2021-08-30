using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserReportsApp.Api.Entities;
using UserReportsApp.Api.Services;
using UserReportsApp.Shared.Models;

namespace UserReportsApp.Api.Controllers
{
    [EnableCors("WebClient")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IUserReportsService _userReportsService;

        public ReportsController(
            IUserReportsService userReportsService)
        {
            _userReportsService = userReportsService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Report), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Report>> CreateReport(ReportDto reportDto)
        {
            var result = await _userReportsService.CreateReportAsync(reportDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(CreateReport), result.Object);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateReport(int id, ReportDto reportDto)
        {
            var result = await _userReportsService.UpdateReportAsync(id, reportDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var result = await _userReportsService.DeleteUserAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }
    }
}
