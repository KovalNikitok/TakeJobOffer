namespace TakeJobOffer.DAL.Entities
{
    public class ProfessionEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public List<SkillEntity> Skills { get; set; } = [];
        public List<ProfessionSkillEntity> ProfessionSkills { get; set; } = [];
    }
}
