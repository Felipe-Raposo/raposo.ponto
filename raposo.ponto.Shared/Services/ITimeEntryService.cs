using raposo.ponto.Shared.DTOs;

namespace raposo.ponto.Shared.Services;

public interface ITimeEntryService
{
    Task<TimeEntryResponseDto> CreateTimeEntryAsync(TimeEntryDto timeEntryDto);
    Task<TimeEntryResponseDto?> GetTimeEntryAsync(int id);
    Task<IEnumerable<TimeEntryResponseDto>> GetTimeEntriesAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<TimeEntryResponseDto?> UpdateTimeEntryAsync(int id, TimeEntryDto timeEntryDto);
    Task<bool> DeleteTimeEntryAsync(int id);
}