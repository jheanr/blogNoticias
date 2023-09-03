#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlogPetNews.API/BlogPetNews.API.csproj", "BlogPetNews.API/"]
RUN dotnet restore "BlogPetNews.API/BlogPetNews.API.csproj"
COPY . .
WORKDIR "/src/BlogPetNews.API"
RUN dotnet build "BlogPetNews.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogPetNews.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogPetNews.API.dll"]