FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SBA-BACKEND.csproj", "./"]
RUN dotnet restore "SBA-BACKEND.csproj"
COPY . .
RUN dotnet publish "SBA-BACKEND.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SBA-BACKEND.dll"]
