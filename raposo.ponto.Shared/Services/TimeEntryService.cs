using Microsoft.EntityFrameworkCore;
using raposo.ponto.Shared.Data;
using raposo.ponto.Shared.DTOs;
using raposo.ponto.Shared.Models;

namespace raposo.ponto.Shared.Services;

public class TimeEntryService : ITimeEntryService
{
    private readonly ApplicationDbContext _context;

    public TimeEntryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TimeEntryResponseDto> CreateTimeEntryAsync(TimeEntryDto timeEntryDto)
    {
        var timeEntry = MapFromDto(timeEntryDto);

        _context.TimeEntries.Add(timeEntry);
        await _context.SaveChangesAsync();

        return MapToResponseDto(timeEntry);
    }

    public async Task<TimeEntryResponseDto?> GetTimeEntryAsync(int id)
    {
        var timeEntry = await _context.TimeEntries.FindAsync(id);
        return timeEntry != null ? MapToResponseDto(timeEntry) : null;
    }

    public async Task<IEnumerable<TimeEntryResponseDto>> GetTimeEntriesAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.TimeEntries.AsQueryable();

        if (startDate.HasValue)
            query = query.Where(t => t.CheckIn.Date >= startDate.Value.Date);

        if (endDate.HasValue)
            query = query.Where(t => t.CheckIn.Date <= endDate.Value.Date);

        var timeEntries = await query.OrderByDescending(t => t.CheckIn).ToListAsync();
        return timeEntries.Select(MapToResponseDto);
    }

    public async Task<TimeEntryResponseDto?> UpdateTimeEntryAsync(int id, TimeEntryDto timeEntryDto)
    {
        var timeEntry = await _context.TimeEntries.FindAsync(id);

        if (timeEntry == null)
            return null;

        timeEntry.CheckIn = timeEntryDto.CheckIn;
        timeEntry.CheckOut = timeEntryDto.CheckOut;
        timeEntry.TransitTimeMinutes = timeEntryDto.TransitTimeMinutes;
        timeEntry.Workplace = timeEntryDto.Workplace;
        timeEntry.Observations = timeEntryDto.Observations;
        timeEntry.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToResponseDto(timeEntry);
    }

    public async Task<bool> DeleteTimeEntryAsync(int id)
    {
        var timeEntry = await _context.TimeEntries.FindAsync(id);

        if (timeEntry == null)
            return false;

        _context.TimeEntries.Remove(timeEntry);
        await _context.SaveChangesAsync();

        return true;
    }

    private static TimeEntryResponseDto MapToResponseDto(TimeEntry timeEntry) =>
        new(
            timeEntry.Id,
            timeEntry.CheckIn,
            timeEntry.CheckOut,
            timeEntry.TransitTimeMinutes,
            timeEntry.Workplace,
            timeEntry.Observations,
            timeEntry.CreatedAt,
            timeEntry.UpdatedAt
        );

    private static TimeEntry MapFromDto(TimeEntryDto timeEntryDto) =>
        new()
        {
            CheckIn = timeEntryDto.CheckIn,
            CheckOut = timeEntryDto.CheckOut,
            TransitTimeMinutes = timeEntryDto.TransitTimeMinutes,
            Workplace = timeEntryDto.Workplace,
            Observations = timeEntryDto.Observations
        };
}