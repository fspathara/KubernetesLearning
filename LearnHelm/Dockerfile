#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LearnHelm/LearnHelm.csproj", "LearnHelm/"]
RUN dotnet restore "LearnHelm/LearnHelm.csproj"
COPY . .
WORKDIR "/src/LearnHelm"
RUN dotnet build "LearnHelm.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LearnHelm.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LearnHelm.dll"]