using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.UseCases.CreateUser;
using BlogPetNews.API.Domain.UseCases.LoginUser;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.News;
using BlogPetNews.API.Infra.Users;
using BlogPetNews.API.Infra.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BlogPetNewsDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<BlogPetNewsDbContext>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BlogPetNewsDbContext>();
        var created = context.Database.EnsureCreated();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var news = new[]
{
    "Caramelo na praia", "Caramelo na moeda", "Caramelo na TI", "Caramelo na europa"
};

app.MapGet("/news", () =>
{
    return news;
})
.WithName("GetNews")
.WithOpenApi();

app.MapPost("/login", (IMediator mediator,string email, string password) =>
{
    var loginCommand = new LoginUserCommand { Email = email, Password = password };
    var login = mediator.Send(loginCommand);
    return login;
}).AllowAnonymous();

app.MapPost("/create", (IMediator mediator, User user) =>
{
    var createCommand = new CreateUserCommand { user = user };
    var created = mediator.Send(createCommand);
    return created;
}).AllowAnonymous();

app.Run();
