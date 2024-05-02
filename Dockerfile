FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish PDFService.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "PDFService.API.dll"]