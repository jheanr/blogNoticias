var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

app.Run();
