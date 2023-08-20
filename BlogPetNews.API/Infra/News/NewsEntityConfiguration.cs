using BlogPetNews.API.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPetNews.API.Infra.News
{
    public class NewsEntityConfiguration
    {

        public NewsEntityConfiguration(EntityTypeBuilder<Domain.News.News> entityBuilder)
        {
            entityBuilder.HasKey(prop => prop.Id);

            entityBuilder.Property(prop => prop.Title).IsRequired().HasMaxLength(100);
            entityBuilder.Property(prop => prop.Content).IsRequired();
            entityBuilder.Property(e => e.UserId);

        }
    }
}
