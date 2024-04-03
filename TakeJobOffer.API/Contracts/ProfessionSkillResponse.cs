namespace TakeJobOffer.API.Contracts
{
    public record ProfessionSkillResponse(Guid SkillId, int SkillMentionCount = 0);
    public record ProfessionSkillFullResponse(Guid ProfessionId, Guid SkillId, int SkillMentionCount = 0);
}
