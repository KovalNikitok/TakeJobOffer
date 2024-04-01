namespace TakeJobOffer.DAL.Entities
{
    public class ProfessionsSkillsEntity
    {
        public Guid ProfessionId { get; set; }
        public Guid SkillId { get; set; }
        public ProfessionEntity Profession { get; set; } = null!;
        public SkillEntity Skill { get; set; } = null!;

        public int SkillMentionCount { get; set; } = 0;
    }
}
