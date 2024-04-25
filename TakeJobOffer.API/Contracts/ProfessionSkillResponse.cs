namespace TakeJobOffer.API.Contracts
{
    public record ProfessionSkillResponse(Guid SkillId, int MentionCount = 0);
    public record ProfessionSkillWithNameResponse(Guid SkillId, string Name, int MentionCount = 0);
    public record ProfessionSkillFullResponse(Guid ProfessionId, Guid SkillId, int MentionCount = 0);
}
