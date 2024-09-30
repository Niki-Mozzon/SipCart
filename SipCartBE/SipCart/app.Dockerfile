# BUILD THE IMAGE
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# TEST
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS tests
WORKDIR /app
COPY ["SipCartApi", "SipCartApi/"]
COPY ["SipCartCore", "SipCartCore/"]
COPY ["SipCartTesting", "SipCartTesting/"]
COPY ["SipCart.sln","./"]
CMD ["dotnet", "test", "--verbosity:minimal"]

# Copy solution and project files
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SipCartApi/SipCartApi.csproj", "SipCartApi/"]
COPY ["SipCartCore/SipCartCore.csproj", "SipCartCore/"]
RUN dotnet restore "SipCartApi/SipCartApi.csproj"
COPY . .
WORKDIR /src/SipCartApi
RUN dotnet build "./SipCartApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SipCartApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SipCartApi.dll"]