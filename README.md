# GameStore - REST API with .NET & Vertical Slice Architecture

I built this project as part of a course to practice and demonstrate modern .NET backend development. My goal was to create a clean, maintainable, and robust API by applying several key architectural patterns and technologies.

## What I Learned and Applied

### Solid REST API Design
I focused on building a proper RESTful API by using correct HTTP verbs and status codes. I also implemented consistent response formats and error handling. For the endpoints, I used .NET's **Minimal APIs** to keep them lightweight and simple.

### Vertical Slice Architecture
I structured the entire application using Vertical Slice Architecture. Instead of traditional layers, I organized all the code for a single feature (its logic, models, and data access) into a self-contained "slice." This approach made the codebase much easier to navigate and helped me make changes without breaking other features.

### Decoupling with Dependency Injection
To keep the code clean, I relied on .NET's built-in dependency injection. I programmed against interfaces instead of concrete classes, which let the IoC container manage dependencies automatically. This made my code more flexible, maintainable, and easier to test.

### Data Access with Entity Framework Core
For data access, I used **Entity Framework Core** with a Code-First approach. I defined my database schema using C# classes and let EF Core generate the database with migrations. I wrote all my queries using type-safe **LINQ**, which let EF handle the SQL generation and change tracking for me.

## Getting Started

1.  Clone this repository.
2.  Add your database connection string to `appsettings.Development.json`.
3.  Run `dotnet ef database update` in the terminal to create the database.
4.  Run `dotnet run` to start the application.
