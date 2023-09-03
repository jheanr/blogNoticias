using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Domain.Utils;

namespace BlogPetNews.API.Domain.News
{
    public class Notice : ClassBase
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Notice() { }

        public Notice(string title, string content, Guid userId) : base()
        {
            Title = title;
            Content = content;
            UserId = userId;
        }


    }
}
