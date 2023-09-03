using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPetNews.API.Infra.News
{
    public class NewsEntityConfiguration : IEntityTypeConfiguration<Domain.News.News>
    {
        public void Configure(EntityTypeBuilder<Domain.News.News> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Title).IsRequired().HasMaxLength(100);
            builder.Property(prop => prop.Content).IsRequired();
            builder.Property(prop => prop.UserId);
        }
    }
}
