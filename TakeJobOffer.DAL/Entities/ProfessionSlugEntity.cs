namespace TakeJobOffer.DAL.Entities
{
    public class ProfessionSlugEntity
    {
        public Guid Id { get; set; }
        public string Slug { get; set; } = string.Empty;

        public Guid ProfessionForeignKey { get; set; }
        public ProfessionEntity Profession { get; set; } = null!;
    }
}
