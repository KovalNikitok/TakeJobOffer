namespace TakeJobOffer.API.Contracts
{
    public record ProfessionResponse(
        Guid Id,
        string Name,
        string? Description);

    public record ProfessionWithSlugResponse(
        Guid Id,
        string Name,
        string? Description,
        string Slug);
}
