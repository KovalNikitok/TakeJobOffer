using FluentResults;

namespace TakeJobOffer.Domain.Models
{
    public class ProfessionSkill
    {
        public const int MIN_PROFESION_SKILL_MENTION = 0;
        private ProfessionSkill(Guid professionId, Guid skillId, int skillMentionCount)
        {
            ProfessionId = professionId;
            SkillId = skillId;
            SkillMentionCount = skillMentionCount;
        }

        public static Result<ProfessionSkill> CreateProfessionSkill(Guid professionId, Guid skillId, int skillMentionCount)
        {
            Result<ProfessionSkill> result = new();

            if (professionId == Guid.Empty || skillId == Guid.Empty)
                result.WithError($"{nameof(ProfessionSkill)} need to have not empty profession Id and skill Id");
            if (skillMentionCount < MIN_PROFESION_SKILL_MENTION)
                result.WithError($"SkillMentionCount can not be less then {MIN_PROFESION_SKILL_MENTION}");

            if (result.IsSuccess)
                result.WithValue(new ProfessionSkill(professionId, skillId, skillMentionCount));

            return result;
        }

        public Guid ProfessionId {  get; }
        public Guid SkillId { get; }
        public int SkillMentionCount { get; } = MIN_PROFESION_SKILL_MENTION;
    }
}
