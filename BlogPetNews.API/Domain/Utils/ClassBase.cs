namespace BlogPetNews.API.Domain.Util
{
    public class ClassBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
