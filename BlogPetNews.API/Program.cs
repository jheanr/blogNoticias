using BlogPetNews.API.Extensions;
using BlogPetNews.API.Infra.Utils;
using BlogPetNews.API.Presentation.News;
using BlogPetNews.API.Presentation.Users;
using Microsoft.OpenApi.Models;
using System.Text;

using MediatR;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

// Add services to the container.
builder.Services.AddAuthJWT(key);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenJwt("v1",
    new OpenApiInfo
    {
        Title = "Pós Tech Fase 2 - Tech Challange",
        Description = "Tech Challenge implementado por: Daniela Miranda de Almeida, Jhean Ricardo Ramos, Lucas dos anjos Varela, Marcelo de Moraes Andrade e Wellington Chida de Oliveira.",
        Version = "v1",
    });
builder.Services.AddDbServices(builder.Configuration);
builder.Services.AddTransient<TokenService>();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.AddUserEndpoints();
app.AddNewsEndpoints();

app.Run();
