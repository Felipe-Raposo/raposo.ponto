namespace raposo.ponto.Shared.DTOs;

public record TimeEntryDto(
    DateTime CheckIn,
    DateTime? CheckOut,
    int TransitTimeMinutes,
    string Workplace,
    string? Observations
);

public record TimeEntryResponseDto(
    int Id,
    DateTime CheckIn,
    DateTime? CheckOut,
    int TransitTimeMinutes,
    string Workplace,
    string? Observations,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);