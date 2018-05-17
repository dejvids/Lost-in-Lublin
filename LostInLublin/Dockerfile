FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY LostInLublin.sln ./
COPY LostInLublin/LostInLublin.csproj LostInLublin/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/LostInLublin
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LostInLublin.dll"]
