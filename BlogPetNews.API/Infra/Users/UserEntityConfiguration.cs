using BlogPetNews.API.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPetNews.API.Infra.Users
{
    public class UserEntityConfiguration
    {
        public UserEntityConfiguration(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(prop => prop.Id);

            entityBuilder.HasIndex(prop => prop.Email).IsUnique();
            entityBuilder.Property(prop => prop.Name).IsRequired().HasMaxLength(200);
            entityBuilder.Property(prop => prop.Password).IsRequired();
            entityBuilder.Property(prop => prop.Role).IsRequired();

            entityBuilder.HasMany(prop => prop.News).WithOne(prop => prop.User).HasForeignKey(prop => prop.UserId).HasPrincipalKey(prop => prop.Id);


        }
    }
}
