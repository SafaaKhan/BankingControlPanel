# Banking Control Panel

## Project Architecture

1. **Main Project**: Banking Control Panel

2. **Class Libraries**:
    - **Access Data**: Includes migration files and repositories.
    - **Models**: Contains models, DTOs, pagination, validation attributes, and static data files.
    - **Utilities**: Includes extensions, helper classes, middleware (ExceptionMiddleware for global error handling), and services (e.g., token service for JWT Bearer=> SigningCredentials with SecurityAlgorithms=>HmacSha512Signature).

3. **Repository**: Used for client CRUD operations.

4. **Global Error Handling**: Implemented for consistent error management across the application.

## Project Libraries

1. [libphonenumber-csharp](https://www.nuget.org/packages/libphonenumber-csharp)
2. Microsoft.AspNetCore.OpenApi
3. Microsoft.EntityFrameworkCore.Design
4. Swashbuckle.AspNetCore
5. Microsoft.AspNetCore.Authentication.JwtBearer
6. Microsoft.AspNetCore.Identity.EntityFrameworkCore
7. Microsoft.EntityFrameworkCore.SqlServer
8. Microsoft.EntityFrameworkCore.Tools
9. Microsoft.Extensions.Configuration
10. AutoMapper
11. Microsoft.Extensions.DependencyInjection
12. Swashbuckle.AspNetCore.Annotations

## Documentation and Testing

1. **Swagger**: For interactive API documentation. 
2. **Postman**: 1- Postman variables and scripts are used for an easier testing process. 2- Pagination also set in the Headers. 
