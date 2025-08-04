# NZ Walks API

A RESTful API for managing walking trails in New Zealand, built with ASP.NET Core and Entity Framework Core.

##  Architecture

This project follows Clean Architecture principles with the following layers:

- **API Layer**: Controllers and DTOs for handling HTTP requests
- **Domain Layer**: Core business entities and models
- **Data Layer**: Entity Framework Core with Repository pattern
- **Infrastructure**: Database context and configurations

### Key Features

- CRUD operations for Walks, Regions, and Difficulties
- Filtering and sorting capabilities
- Model validation with Data Annotations
- Repository pattern implementation
- Async/await throughout
- Entity Framework Core with SQL Server

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Language**: C# 12
- **Architecture**: Repository Pattern + Clean Architecture

##  Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or full instance)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [JetBrains Rider](https://www.jetbrains.com/rider/)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Sonseldeep/Walk.Api
cd Walk.Api
```
### 2. Configuration Database Connection

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=NZWalksDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 3. Apply Database Migrations

```bash
dotnet ef database update
```

### 4. Run the Application

```bash
    dotnet run --project NZWalks.Api
```

### API Endpoints
###Regions

```bash
GET /api/regions - Get all regions
GET /api/regions/{id} - Get region by ID
POST /api/regions - Create new region
PUT /api/regions/{id} - Update region
DELETE /api/regions/{id} - Delete region

```
#### Walks
``` bash

GET /api/walks - Get all walks (supports filtering & sorting)
GET /api/walks/{id} - Get walk by ID
POST /api/walks - Create new walk
PUT /api/walks/{id} - Update walk
DELETE /api/walks/{id} - Delete walk
```

### Query Parameters for Walks

```bash
filterOn=Name&filterQuery=searchterm - Filter by name
sortBy=Name&isAscending=true - Sort by name
sortBy=Length&isAscending=false - Sort by length
```

### Install Dependencies & Run
```bash
dotnet restore
```
### Install Required Packages
```bash
# Entity Framework Core packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design

# ASP.NET Core packages
dotnet add package Microsoft.AspNetCore.OpenApi
dotnet add package Swashbuckle.AspNetCore
```
### Set Up Database
```bash
# Create initial migration (if not exists)
dotnet ef migrations add InitialCreate

# Update database with migrations
dotnet ef database update
```
### Run the Project
```bash
dotnet run --project NZWalks.Api
```

### Clean and Rebuild
```bash
dotnet clean
dotner build
# Rub with specific environment
dotnet run --environment Development

# Watch for changes (hot reload)
dotnet watcg run --project NZWalks.Api
```
### Testing
```bash
dotnet test
```
