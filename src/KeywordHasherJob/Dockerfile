FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["KeywordHasherJob/KeywordHasherJob.csproj", "KeywordHasherJob/"]
COPY ["NuGet.Config", "."]
RUN dotnet restore "KeywordHasherJob/KeywordHasherJob.csproj" --configfile ./NuGet.Config
COPY . .
WORKDIR "/src/KeywordHasherJob"
RUN dotnet build "KeywordHasherJob.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KeywordHasherJob.csproj" -c Release -o /app/publish

FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeywordHasherJob.dll"]
