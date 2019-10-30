FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DemoUserSaveAPI/DemoUserSaveAPI.csproj", "DemoUserSaveAPI/"]
COPY ["DemoUserSaveAPILibs.Core/DemoUserSaveAPILibs.Core.csproj", "DemoUserSaveAPILibs.Core/"]
RUN dotnet restore "DemoUserSaveAPI/DemoUserSaveAPI.csproj"
COPY . .
WORKDIR "/src/DemoUserSaveAPI"
RUN dotnet build "DemoUserSaveAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoUserSaveAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoUserSaveAPI.dll"]
