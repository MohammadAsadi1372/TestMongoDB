#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TestMongoDB/TestMongoDB.csproj", "TestMongoDB/"]
RUN dotnet restore "TestMongoDB/TestMongoDB.csproj"
COPY . .
WORKDIR "/src/TestMongoDB"
RUN dotnet build "TestMongoDB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestMongoDB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestMongoDB.dll"]