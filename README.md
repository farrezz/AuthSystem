# AuthSystem API

A RESTful authentication API built with ASP.NET Core 8 that provides user registration, login, and JWT-based token authentication.

## Features

- **User Registration** - Create new user accounts with secure password hashing
- **User Login** - Authenticate users and receive JWT tokens
- **JWT Authentication** - Secure API endpoints with JWT bearer tokens
- **Password Security** - BCrypt password hashing and verification
- **Swagger Integration** - Interactive API documentation with JWT support for testing protected endpoints

## Tech Stack

- **Framework**: ASP.NET Core 8
- **Database**: SQL Server (LocalDB)
- **Authentication**: JWT Bearer tokens
- **Password Hashing**: BCrypt
- **ORM**: Entity Framework Core
- **API Documentation**: Swagger/OpenAPI

## Required NuGet Packages

The project includes the following NuGet packages (automatically restored on build):

- `BCrypt.Net-Next` (4.1.0) - Password hashing
- `Microsoft.AspNetCore.Authentication.JwtBearer` (8.0.11) - JWT authentication
- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.11) - SQL Server support
- `Microsoft.EntityFrameworkCore.Tools` (8.0.11) - EF Core migrations
- `Swashbuckle.AspNetCore` (8.1.1) - Swagger/OpenAPI documentation

## Setup Instructions

### 1. Prerequisites

- .NET 8 SDK or later
- SQL Server LocalDB (installed with Visual Studio)
- Visual Studio or VS Code

### 2. Clone the Repository

```bash
git clone <repository-url>
cd AuthSystem
```

### 3. Database Setup with SSMS

1. **Connect to LocalDB in SSMS**
   - Open SQL Server Management Studio
   - Connect to: `(localdb)\mssqllocaldb`

2. **Create Database and Tables**
   - Open Visual Studio Package Manager Console
   - Run the following commands:

   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

3. **Verify in SSMS**
   - Refresh the databases in SSMS
   - You should see `AuthSystemDb` with a `Users` table

### 4. Configure JWT Settings

Update `appsettings.json` with your JWT configuration:

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "AuthSystem.API",
    "Audience": "AuthSystem.Client",
    "ExpirationInMinutes": 60
  }
}
```

> ⚠️ Change the JWT key to a strong, unique secret before deploying to production.

### 5. Run the Application

```bash
dotnet run
```

The API will start at `https://localhost:5001`

## API Endpoints

### Authentication

- **POST** `/api/auth/register` - Register a new user
  ```json
  Request: { "username": "john", "email": "john@example.com", "password": "SecurePass123!" }
  Response: { "token": "eyJhbGc...", "username": "john", "email": "john@example.com", "expiresAt": "..." }
  ```

- **POST** `/api/auth/login` - Login and receive JWT token
  ```json
  Request: { "username": "john", "password": "SecurePass123!" }
  Response: { "token": "eyJhbGc...", "username": "john", "email": "john@example.com", "expiresAt": "..." }
  ```

## Testing with Swagger

1. Navigate to `https://localhost:5001/swagger`
2. Click the **Authorize** button (top right)
3. Paste your JWT token from a login/register response
4. Test protected endpoints directly

## Project Structure

```
AuthSystem.API/
├── Controllers/       - API endpoints
├── Models/           - User entity
├── Services/         - Business logic (Auth, Token, Password)
├── Data/            - DbContext configuration
├── DTOs/            - Data transfer objects
├── Configuration/   - JWT settings
├── Migrations/      - EF Core migrations
└── appsettings.json - Configuration
```
