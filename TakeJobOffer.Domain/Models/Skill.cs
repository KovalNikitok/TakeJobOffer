using FluentResults;

namespace TakeJobOffer.Domain.Models
{
    public class Skill
    {
        public const int MAX_NAME_LENGTH = 128;
        public Guid Id { get; }
        public string Name { get; } = string.Empty;

        private Skill(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Skill> Create(Guid id, string name)
        {
            var result = new Result<Skill>();
            if (id == Guid.Empty)
                result.WithError(new Error("Skill need to have not empty Id"));
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
                result.WithError(new Error($"Name can not be empty or more then {MAX_NAME_LENGTH} symbols"));

            if(result.IsSuccess)
                result.WithValue(new Skill(id, name));

            return result;
        }
    }
}
