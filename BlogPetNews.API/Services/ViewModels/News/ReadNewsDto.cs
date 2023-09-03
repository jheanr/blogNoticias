using BlogPetNews.API.Service.ViewModels.Users;

namespace BlogPetNews.API.Service.ViewModels.News
{
    public class ReadNewsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public ReadUserDto Author { get; set; }

    }
}
