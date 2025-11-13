# GameStore - REST API with .NET & Vertical Slice Architecture
I built this project as part of a course to practice and demonstrate modern .NET backend development. My goal was to create a clean, maintainable, and robust API by applying several key architectural patterns and production-ready techniques.

## What I Learned and Applied
This project covers both the foundational architecture of a modern API and the advanced patterns required to make it production-ready.

## Core Architecture & Design

### Solid REST API Design
I focused on building a proper RESTful API by using correct HTTP verbs and status codes. I also implemented consistent response formats and error handling. For the endpoints, I used .NET's **Minimal APIs** to keep them lightweight and simple.

### Vertical Slice Architecture
I structured the entire application using Vertical Slice Architecture. Instead of traditional layers, I organised all the code for a single feature (its logic, models, and data access) into a self-contained "slice." This approach made the codebase much easier to navigate and helped me make changes without breaking other features.

### Decoupling with Dependency Injection
To keep the code clean, I relied on .NET's built-in dependency injection. I programmed against interfaces instead of concrete classes, which let the IoC container manage dependencies automatically. This made my code more flexible, maintainable, and easier to test.

### Data Access with Entity Framework Core
For data access, I used **Entity Framework Core** with a Code-First approach. I defined my database schema using C# classes and let EF Core generate the database with migrations. I wrote all my queries using type-safe **LINQ**, which let EF handle the SQL generation and change tracking for me.

## Production-Ready Enhancements

### Asynchronous Programming for Scalability:
- Used async/await patterns for all I/O-bound operations (like database queries) to prevent blocking threads.
- This dramatically improves scalability, allowing the API to handle thousands of concurrent requests efficiently.
- Frees up server resources, enabling the app to serve more users with the same hardware.

### Production-Ready Logging:
- Utilised the built-in ASP.NET Core logging infrastructure (ILogger, ILoggerFactory) to capture diagnostic information.
- Learned to distinguish between different logging contexts (e.g., WebApplication logger vs. injected ILogger).
- Configured log levels (e.g., Information, Warning, Error) to control logging verbosity in different environments (like Debug vs. Production).

### Cross-Cutting Concerns with Middleware:
- Created custom middleware classes to handle reusable, cross-cutting logic (like global error handling) in one place.
- Mastered the request pipeline order to ensure middleware runs correctly.
- Leveraged built-in middleware, like ASP.NET Core's HTTP logging, for easy request/response diagnostics.

### Global Error Handling:
- Implemented a global exception handling middleware to catch all unhandled errors, removing the need for scattered try-catch blocks.
- Added support for Problem Details (RFC 7807) to return standardized, machine-readable error responses that clients can reliably parse.
- Ensured that sensitive error details are logged for debugging but never exposed to the client.

### Efficient Pagination & Search:
- Delivered fast API responses for large datasets by implementing pagination using LINQ's .Skip() and .Take() operators.
- This prevents overwhelming clients with massive JSON payloads.
- Added simple, case-insensitive search functionality for a better user experience.

### Secure File Uploads:
- Built an endpoint to handle file uploads safely, including validation for file type and size to prevent malicious uploads.
- Implemented logic to generate unique file names, avoiding conflicts and overwrites.
- Configured the application to securely serve the uploaded files (e.g., images) back to the user.

## Getting Started
1.  Clone this repository.
2.  Run `dotnet ef database update` in the terminal to create the database.
3.  Run `dotnet run` to start the application.
