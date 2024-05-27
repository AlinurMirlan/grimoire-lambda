# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["./Grimoire.Lambda.csproj", "./"]
RUN dotnet restore

# Copy the remaining source code and build the application
COPY . .
RUN dotnet publish -c Release -o /app

# Stage 2: Create the runtime image
FROM public.ecr.aws/lambda/dotnet:8 AS runtime

# Copy the compiled application from the build stage
COPY --from=build /app /var/task

# Specify the Lambda function handler
CMD ["Grimoire.Lambda::Grimoire.Lambda.Function::FunctionHandler"]