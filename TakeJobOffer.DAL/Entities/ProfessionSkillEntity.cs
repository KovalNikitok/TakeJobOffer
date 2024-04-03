namespace TakeJobOffer.DAL.Entities
{
    public class ProfessionSkillEntity
    {
        public Guid ProfessionForeignKey { get; set; }
        public Guid SkillForeignKey { get; set; }

        public ProfessionEntity Profession { get; set; } = null!;
        public SkillEntity Skill { get; set; } = null!;

        public int SkillMentionCount { get; set; } = 0;
    }
}
