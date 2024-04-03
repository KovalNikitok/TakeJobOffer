namespace TakeJobOffer.API.Contracts
{
    public record ProfessionSkillRequest(Guid SkillId);
    public record ProfessionSkillWithSkillMentionRequest(Guid SkillId, int SkillMentionCount = 0);
}
