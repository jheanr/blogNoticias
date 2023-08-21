using BlogPetNews.API.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPetNews.API.Infra.Users
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.HasIndex(prop => prop.Email).IsUnique();
            builder.Property(prop => prop.Name).IsRequired().HasMaxLength(200);
            builder.Property(prop => prop.Password).IsRequired();
            builder.Property(prop => prop.Role).IsRequired();

            builder.HasMany(prop => prop.News)
                .WithOne(prop => prop.User)
                .HasForeignKey(prop => prop.UserId)
                .HasPrincipalKey(prop => prop.Id);
        }
    }
}
