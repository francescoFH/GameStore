namespace GameStore.Api.Features.Games;

public record GameSummaryDto(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleseDate
);
