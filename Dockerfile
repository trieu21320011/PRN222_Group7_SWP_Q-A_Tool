# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["PRN222_Group7_SWP_QA_Tool.sln", "."]
COPY ["SWP_QA_TOOL/SWP_QA_TOOL.csproj", "SWP_QA_TOOL/"]
COPY ["BussinessLayer/BussinessLayer.csproj", "BussinessLayer/"]
COPY ["DataLayer/DataLayer.csproj", "DataLayer/"]

# Restore dependencies
RUN dotnet restore "SWP_QA_TOOL/SWP_QA_TOOL.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/SWP_QA_TOOL"
RUN dotnet build "SWP_QA_TOOL.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "SWP_QA_TOOL.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published files
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "SWP_QA_TOOL.dll"]
