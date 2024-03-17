namespace TakeJobOffer.Domain.Models
{
    public class Profession
    {
        const int MAX_NAME_LENGTH = 255; 
        const int MAX_DESCRIPTION_LENGTH = 255;
        private Profession(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;

        public List<Skill> Skills { get; } = new();

        public static (Profession Profession, string Error) Create(Guid id, string name, string description)
        {
            var error = string.Empty;

            if (id == Guid.Empty)
                error = "Profession need to have not empty Id";
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
                error = $"Name can not be empty or more then {MAX_NAME_LENGTH} symbols";
            if(description.Length > MAX_DESCRIPTION_LENGTH)
                error = $"Description can not be more then {MAX_DESCRIPTION_LENGTH} symbols";

            var profession = new Profession(id, name, description);

            return (profession, error);
        }
    }
}
