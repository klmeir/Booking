# Booking API with .NET

[![CI/CD](https://github.com/klmeir/Booking/actions/workflows/master.yml/badge.svg)](https://github.com/klmeir/Booking/actions/workflows/master.yml)

## Collection
[<img src="https://run.pstmn.io/button.svg" alt="Run In Postman" style="width: 128px; height: 32px;">](https://app.getpostman.com/run-collection/17436300-f78a841a-6ece-4f6f-b4e7-342b5f1b1f4f?action=collection%2Ffork&source=rip_markdown&collection-url=entityId%3D17436300-f78a841a-6ece-4f6f-b4e7-342b5f1b1f4f%26entityType%3Dcollection%26workspaceId%3D8dceb780-7c6a-44a3-bba7-90c59549f93b)

## Swagger
https://nitro-booking.azurewebsites.net/swagger/index.html

## Database schema

<img src="https://github.com/klmeir/booking/blob/master/docs/ER.png" />

## Continuous integration

<img src="https://github.com/klmeir/booking/blob/master/docs/CI.png" />

## Continuous deployment

<img src="https://github.com/klmeir/booking/blob/master/docs/CD.png" />
<img src="https://github.com/klmeir/booking/blob/master/docs/CI-CD.png" />

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
- Unit tests
- CI/CD

### Project structure:

Solution for VisualStudio(.sln) composed of the following folders :

- Api : Api Rest, entry point of the application
- Application : Domain Services Orchestration Layer; Ports, Commands, Queries, Handlers
- Infrastructure : Adapters
- Domain : Entities, Value Objects, Ports, Domain Services, Aggregates
- Domain.Tests : Unit Tests para Domain Services

# Build & Run

## Visual Studio 2022

To run the project open the solution in visual studio, check the database connection string and run.

## Docker and Docker Compose

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
