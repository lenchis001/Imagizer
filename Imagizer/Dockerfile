#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Imagizer/Imagizer.csproj", "Imagizer/"]
RUN dotnet restore "Imagizer/Imagizer.csproj"
COPY . .
WORKDIR "/src/Imagizer"
RUN dotnet build "Imagizer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Imagizer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imagizer.dll"]