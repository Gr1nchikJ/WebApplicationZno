# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy only the .csproj files and restore dependencies.
# This allows for better caching when only the project files change.
COPY *.csproj ./
RUN dotnet restore

# Copy the entire source code and build the application in Release mode.
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Create a minimal runtime image
# Use a smaller and lightweight base image for the final image.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final-env
WORKDIR /app

# Copy only the published output from the build stage.
COPY --from=build-env /app/out .

# Expose ports (not mandatory, but good practice to document)
EXPOSE 80
EXPOSE 443

# Set environment variables (if needed)
# ENV VARIABLE_NAME=VALUE

# Set any other configuration you might need (if needed)
# RUN some_configuration_command

# Start the application with the specified entry point.
ENTRYPOINT ["dotnet", "WebApplicationZno.dll"]