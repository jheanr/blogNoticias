namespace BlogPetNews.API.Domain.Utils
{
    public class ClassBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
