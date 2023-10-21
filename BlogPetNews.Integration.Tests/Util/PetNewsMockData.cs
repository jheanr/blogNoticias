using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPetNews.Integration.Tests.Util
{
    public class PetNewsMockData
    {
        public static async Task CreateUser(PetNewsApiApplication application, bool create)
        {
            var cryptography = new Cryptography();

            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var petNewsDbContext = provider.GetRequiredService<BlogPetNewsDbContext>())
                {
                    await petNewsDbContext.Database.EnsureCreatedAsync();

                    if (create)
                    {
                        await petNewsDbContext.Users.AddAsync(new User 
                        { 
                            Name = "El Gato", 
                            Email = "elgato@miau.net", 
                            Password = cryptography.Encodes("gato@123") 
                        });
                        await petNewsDbContext.SaveChangesAsync();
                    }
                }
            }
        }

        public static async Task CreateNews(PetNewsApiApplication application, bool create)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var petNewsDbContext = provider.GetRequiredService<BlogPetNewsDbContext>())
                {
                    await petNewsDbContext.Database.EnsureCreatedAsync();

                    if (create)
                    {
                        await petNewsDbContext.News.AddAsync(new News
                        {
                            Title = "Chocante! Cachorro caramelo faz mortal de skate na pista e sai ileso",
                            Content = "Em um evento surpreendente no Parque Radical Aventura, um cachorro da raça caramelo chamado Jaguarinha realizou uma manobra mortal em uma pista de skate, executando um giro de 720 graus no ar, e saiu ileso. O treinador, Carlos Mendes, compartilhou a incrível jornada de treinamento do cachorro, enquanto preocupações sobre seu bem-estar surgiram. Apesar da controvérsia, o vídeo viralizou, mostrando que o cachorro caramelo conquistou seu lugar na história dos esportes radicais.",
                            DateCreated = DateTime.Now,
                            User = new()
                            {
                                DateCreated = DateTime.Now,
                                Name = "Tony Hawk",
                                Email = "tony-hawk@skate2.com",
                                Password = "",
                            }
                        });

                        await petNewsDbContext.News.AddAsync(new News
                        {
                            Title = "Chocante! Cachorro caramelo faz mortal de skate na pista e sai ileso",
                            Content = "Em um evento surpreendente no Parque Radical Aventura, um cachorro da raça caramelo chamado Jaguarinha realizou uma manobra mortal em uma pista de skate, executando um giro de 720 graus no ar, e saiu ileso. O treinador, Carlos Mendes, compartilhou a incrível jornada de treinamento do cachorro, enquanto preocupações sobre seu bem-estar surgiram. Apesar da controvérsia, o vídeo viralizou, mostrando que o cachorro caramelo conquistou seu lugar na história dos esportes radicais.",
                            DateCreated = DateTime.Now,
                            User = new()
                            {
                                DateCreated = DateTime.Now,
                                Name = "Tony Hawk",
                                Email = "tony-hawk@skate2.com",
                                Password = "",
                            }
                        });

                        await petNewsDbContext.SaveChangesAsync();
                    }
                }
            }
        }

    }
}
