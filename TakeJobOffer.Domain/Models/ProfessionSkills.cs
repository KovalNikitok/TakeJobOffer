using FluentResults;

namespace TakeJobOffer.Domain.Models
{
    public class ProfessionSkills
    {
        public const int MIN_PROFESION_SKILL_MENTION = 0;
        private ProfessionSkills(Guid professionId, Guid skillId, int skillMentionCount)
        {
            ProfessionId = professionId;
            SkillId = skillId;
            SkillMentionCount = skillMentionCount;
        }

        public Result<ProfessionSkills> CreateProfessionSkills(Guid professionId, Guid skillId, int skillMentionCount)
        {
            var result = new Result<ProfessionSkills>();

            if (professionId == Guid.Empty || skillId == Guid.Empty)
                result.WithError($"{nameof(ProfessionSkills)} need to have not empty profession Id and skill Id");
            if (SkillMentionCount < MIN_PROFESION_SKILL_MENTION)
                result.WithError($"SkillMentionCount can not be less then {MIN_PROFESION_SKILL_MENTION}");

            if (result.IsSuccess)
                result.WithValue(new ProfessionSkills(professionId, skillId, skillMentionCount));

            return result;
        }

        public Guid ProfessionId {  get; }
        public Guid SkillId { get; }
        public int SkillMentionCount { get; } = MIN_PROFESION_SKILL_MENTION;
    }
}
