# RestAuthorAPI

A small ASP.NET Core Web API for managing:
- Authors
- Books
- Publishers

The project uses:
- .NET 10
- SQLite via Entity Framework Core

## Prerequisites

Install:
- .NET SDK 10.0

Check your SDK version:

    dotnet --version

## Get Up and Running

From the project root, run:

    dotnet restore
    dotnet build
    dotnet run

By default (Development profile), the API runs on:
- http://localhost:5089
- https://localhost:7157

## OpenAPI

In Development, the OpenAPI document is available at:
- http://localhost:5089/openapi/v1.json
- https://localhost:7157/openapi/v1.json

## API Endpoints

Base route pattern:
- /api/{controller}

Available controllers:
- /api/authors
- /api/books
- /api/publishers

Common operations:
- GET /api/{controller}
- GET /api/{controller}/{id}
- POST /api/{controller}
- PUT /api/{controller}/{id}
- DELETE /api/{controller}/{id}

## Quick Test Commands

Get all books:

    curl http://localhost:5089/api/books

Create an author:

    curl -X POST http://localhost:5089/api/authors \
      -H "Content-Type: application/json" \
      -d "{\"name\":\"Jane\",\"surname\":\"Austen\"}"

Create a publisher:

    curl -X POST http://localhost:5089/api/publishers \
      -H "Content-Type: application/json" \
      -d "{\"name\":\"Penguin\"}"

## Database Notes

The app uses a SQLite file named:
- library.db

If you get table-related SQLite errors (for example "no such table"), initialize the database schema with EF Core migrations.

Install EF tooling (if needed):

    dotnet tool install --global dotnet-ef

Then run:

    dotnet ef migrations add InitialCreate
    dotnet ef database update

## Useful Files

- Program startup/configuration: Program.cs
- DbContext: Data/LibraryContext.cs
- Controllers: Controllers/
- Local HTTP request samples: Library.RestApi.http
