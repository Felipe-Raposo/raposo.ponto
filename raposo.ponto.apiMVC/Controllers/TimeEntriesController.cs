using Microsoft.AspNetCore.Mvc;
using raposo.ponto.Shared.DTOs;
using raposo.ponto.Shared.Services;

namespace raposo.ponto.apiMVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeEntriesController : ControllerBase
{
    private readonly ITimeEntryService _timeEntryService;

    public TimeEntriesController(ITimeEntryService timeEntryService)
    {
        _timeEntryService = timeEntryService;
    }

    [HttpPost]
    public async Task<ActionResult<TimeEntryResponseDto>> CreateTimeEntry(TimeEntryDto timeEntryDto)
    {
        var result = await _timeEntryService.CreateTimeEntryAsync(timeEntryDto);
        return CreatedAtAction(nameof(GetTimeEntry), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TimeEntryResponseDto>> GetTimeEntry(int id)
    {
        var timeEntry = await _timeEntryService.GetTimeEntryAsync(id);
        if (timeEntry == null)
            return NotFound();

        return Ok(timeEntry);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimeEntryResponseDto>>> GetTimeEntries(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var timeEntries = await _timeEntryService.GetTimeEntriesAsync(startDate, endDate);
        return Ok(timeEntries);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TimeEntryResponseDto>> UpdateTimeEntry(int id, TimeEntryDto timeEntryDto)
    {
        var result = await _timeEntryService.UpdateTimeEntryAsync(id, timeEntryDto);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimeEntry(int id)
    {
        var result = await _timeEntryService.DeleteTimeEntryAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}