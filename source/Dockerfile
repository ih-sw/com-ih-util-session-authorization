FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Com.IH.House/Com.IH.House.Api.csproj"
COPY . .
WORKDIR "/src/Com.IH.House.Api"
RUN dotnet build "Com.IH.House.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Com.IH.House.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Com.IH.House.Api.dll"]