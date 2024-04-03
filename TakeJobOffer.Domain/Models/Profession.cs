using FluentResults;

namespace TakeJobOffer.Domain.Models
{
    public class Profession
    {
        public const int MAX_NAME_LENGTH = 255; 
        public const int MAX_DESCRIPTION_LENGTH = 255;
        private Profession(Guid id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        private Profession(Guid id, string name, string? description, List<Skill> skills)
        {
            Id = id;
            Name = name;
            Description = description;
            Skills = skills;
        }

        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string? Description { get; } = string.Empty;

        public List<Skill> Skills { get; } = [];

        public static Result<Profession> Create(Guid id, string name, string? description)
        {
            Result<Profession> result = new();

            if (id == Guid.Empty)
                result.WithError($"{nameof(Profession)} need to have not empty Id");
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
                result.WithError($"Name can not be empty or more then {MAX_NAME_LENGTH} symbols");
            if(description?.Length > MAX_DESCRIPTION_LENGTH)
                result.WithError($"Description can not be more then {MAX_DESCRIPTION_LENGTH} symbols");

            if(result.IsSuccess)
                result.WithValue(new Profession(id, name, description));

            return result;
        }

        public static Result<Profession> Create(Guid id, string name, string? description, List<Skill> skills)
        {
            var result = new Result<Profession>();

            if (id == Guid.Empty)
                result.WithError("Profession need to have not empty Id");
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
                result.WithError($"Name can not be empty or more then {MAX_NAME_LENGTH} symbols");
            if (description?.Length > MAX_DESCRIPTION_LENGTH)
                result.WithError($"Description can not be more then {MAX_DESCRIPTION_LENGTH} symbols");

            if (result.IsSuccess)
                result.WithValue(new Profession(id, name, description, skills));

            return result;
        }
    }
}
