namespace TakeJobOffer.API.Contracts
{
    public record ProfessionRequest(string Name, string? Description);

    public record ProfessionWithSlugRequest(string Name, string? Description, string Slug);
}
