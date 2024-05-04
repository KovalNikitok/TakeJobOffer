namespace TakeJobOffer.API.Contracts
{
    public record ProfessionSlugResponse(
        Guid Id,
        Guid ProfessionId,
        string Slug);
}
