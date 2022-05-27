ARG TARGETPLATFORM=linux/amd64

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
RUN apt-get update --yes --quiet > /dev/null && apt-get upgrade --yes --quiet > /dev/null
RUN apt-get install chromium --quiet --yes

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["screenshot-api/screenshot-api.csproj", "screenshot-api/"]
RUN dotnet restore "screenshot-api/screenshot-api.csproj"
COPY . .
WORKDIR "/src/screenshot-api"
RUN dotnet build "screenshot-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "screenshot-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "screenshot-api.dll"]
