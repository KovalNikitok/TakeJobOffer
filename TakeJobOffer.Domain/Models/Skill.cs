using System.Xml.Linq;

namespace TakeJobOffer.Domain.Models
{
    public class Skill
    {
        const int MAX_NAME_LENGTH = 128;
        public Guid Id { get; }
        public string Name { get; } = string.Empty;

        private Skill(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static (Skill Skill, string Error) Create(Guid id, string name)
        {
            var error = string.Empty;

            if (id == Guid.Empty)
                error = "Skill need to have not empty Id";
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
                error = $"Name can not be empty or more then {MAX_NAME_LENGTH} symbols";

            var skill = new Skill(id, name);

            return (skill, error);
        }
    }
}
