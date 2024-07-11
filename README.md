# Booking API with .NET

# The main architectural patterns and styles that guide this solution are

- Port and Adapter Architecture
- CQRS (Command Query Responsibility Segregation)

# Technical specifications:

- Ready to containerize with Docker.
- Entity Framework Core 6
- Generic Repository (very useful with aggregate management)
- Shadow Properties on entities: Properties that are added to domain entities without "poisoning" the entity's own definition in that layer.
- Automatic Domain Services injection using "[DomainService]" annotation.
- MediaTR : register command handlers and queries automatically (via reflection does scan of the assembly)
- Global Exception Handler
- Logs : Console
- Swagger

### Project structure:

Solution for VisualStudio(.sln) composed of the following folders :

- Api : Api Rest, entry point of the application
- Application : Domain Services Orchestration Layer; Ports, Commands, Queries, Handlers
- Infrastructure : Adapters
- Domain : Entities, Value Objects, Ports, Domain Services, Aggregates
- Function : Message reading function

## Build & Run

# Visual Studio 2022

To run the project open the solution in visual studio, check the database connection string and run.

# Docker and Docker Compose

The execution of docker compose from visual studio is functional, at the moment we are working to execute it from command line...

To startup the whole solution, execute the following command:

```
docker-compose build --no-cache
docker-compose up -d
```

Then the following containers should be running on `docker ps`:

| Application      | URL                                                                                |
| ---------------- | ---------------------------------------------------------------------------------- |
| Booking API      | https://localhost:8000                                                             |
| SQL Server       | Server=localhost;User Id=sa;Password=<YourStrong!Passw0rd>;Database=Booking; |


Browse to [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html) and view the swagger documentation
