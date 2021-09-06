using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
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
        public async Task<ActionResult<Report>> CreateReport([FromBody] ReportListItemDto reportDto)
        {
            var createResult = await _userReportsService.CreateReportAsync(reportDto);

            if (!createResult.Success)
            {
                return BadRequest(createResult.ErrorMessage);
            }

            return CreatedAtAction(nameof(CreateReport), reportDto);
        }

        [HttpPut("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateReport(int id, [FromBody] ReportListItemDto reportDto)
        {
            var updateResult = await _userReportsService.UpdateReportAsync(id, reportDto);

            if (!updateResult.Success)
            {
                return BadRequest(updateResult.ErrorMessage);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteReport(int id)
        {
            var deleteResult = await _userReportsService.DeleteReportAsync(id);

            if (!deleteResult.Success)
            {
                return BadRequest(deleteResult.ErrorMessage);
            }

            return NoContent();
        }


        [HttpGet("users/{userId:int}")]
        public async Task<PagingModel<ReportListItemDto>> GetUserReportsAsync(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            if (page < 1)
            {
                throw new ArgumentException("Параметр \"page\" должен быть больше нуля");
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Параметр \"pageSize\" должен быть больше нуля");
            }

            return await _userReportsService.GetUserReportsAsync(userId, page, pageSize);
        }
    }
}
