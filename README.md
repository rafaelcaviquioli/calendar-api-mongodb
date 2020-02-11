# Calendar WebApi .NET Core

- .NET Core 
- CQRS Architecture
  - MediatR
- EF Core
- Swagger
- Docker
- Unit Tests
  - XUnit
  - Fluent Assertions
 
#### Project docker image is available in Docker Hub
 
[github.com/rafaelcaviquioli/calendar-api-dotnet](https://github.com/rafaelcaviquioli/calendar-api-dotnet)

##### Start project

```bash
$ docker run -it -p 80:80 rafaelcaviquioli/calendar-api-dotnet:latest
```

#### OpenAPI Specification

[http://localhost/swagger](http://localhost/swagger)

#### Run Unit Tests

```bash
$ dotnet test
```

#### Build docker image

```bash
$ docker build -t rafaelcaviquioli/calendar-api-dotnet:latest ./CalendarAPI -f ./Dockerfile
```
