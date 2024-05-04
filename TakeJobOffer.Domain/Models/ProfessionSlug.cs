using FluentResults;

namespace TakeJobOffer.Domain.Models
{
    public class ProfessionSlug
    {
        public const int MAX_SLUG_LENGTH = 64;

        private ProfessionSlug(Guid id, Guid professionId, string slug)
        {
            Id = id;
            ProfessionId = professionId;
            Slug = slug;
        }

        public static Result<ProfessionSlug> CreateProfessionSlug(Guid id, Guid professionId, string slug)
        {
            Result<ProfessionSlug> result = new();
            if (id == Guid.Empty || professionId == Guid.Empty)
                result.WithError($"{nameof(ProfessionSlug)} should have not empty Id and profession Id");
            if (string.IsNullOrWhiteSpace(slug) || slug.Length > MAX_SLUG_LENGTH || slug.Contains(' '))
                result.WithError($"{nameof(ProfessionSlug)} should have not empty slug without spaces symbols and lesser than 64 length");

            if (result.IsSuccess)
                return result.WithValue(new ProfessionSlug(id, professionId, slug));

            return result;
        }

        public Guid Id { get; }
        public Guid ProfessionId { get; }
        public string Slug { get; } = string.Empty;
    }
}
