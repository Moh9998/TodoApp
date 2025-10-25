
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ./src ./src
RUN dotnet restore src/TodoApp.WebApi/TodoApp.WebApi.csproj
RUN dotnet publish src/TodoApp.WebApi/TodoApp.WebApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "TodoApp.WebApi.dll"]
