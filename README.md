# Restaurant Booking System API

A robust ASP.NET Core Web API for managing restaurant operations including table bookings, menu management, customer data, and admin authentication.

## Description

The Restaurant Booking System API is a comprehensive backend solution built with ASP.NET Core 8.0 and Entity Framework Core. It provides secure JWT-based authentication for administrators and handles all restaurant management operations through RESTful endpoints. The API features automatic database seeding with sample data, comprehensive table availability checking, and a clean architecture with repository pattern implementation. Built with Swagger documentation for easy testing and integration.

## Features

- 🔐 **JWT Authentication**: Secure admin authentication with token-based authorization
- 📅 **Booking Management**: Create, update, delete, and view table reservations
- 🪑 **Table Management**: Manage restaurant tables with capacity tracking
- 👥 **Customer Management**: Handle customer information and booking history
- 🍽️ **Menu Management**: Full CRUD operations for menu items with categories
- 📊 **Availability Checking**: Real-time table availability validation
- 📖 **Swagger Documentation**: Interactive API documentation
- 🌱 **Database Seeding**: Pre-populated sample data for testing

## Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB, Express, or full version)
- Visual Studio 2022 or VS Code

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/timsuv/BookingSystem.git
cd BookingSystem
```

### 2. Configure App Settings

Create an `appsettings.json` file in the root directory with the following configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RestaurantBookingDB;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "RestaurantBookingSystem",
    "Audience": "RestaurantBookingSystem"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**Important Configuration Notes:**
- **ConnectionString**: Update to match your SQL Server setup
- **SecretKey**: Must be at least 32 characters long. Change this for production!
- **Issuer/Audience**: Can be customized but should match across environments

### 3. Database Setup

The application uses Entity Framework Code First with automatic database creation:

```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Create and seed the database
dotnet run
```

The database will be automatically created with sample data including:
- Default admin user (username: `admin`, password: `Admin1234!`)
- 5 sample tables with different capacities
- 24 authentic Italian menu items across all categories

### 4. Run the Application

```bash
dotnet restore
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:7189`
- HTTP: `http://localhost:5013`
- Swagger UI: `https://localhost:7189/swagger`

## API Endpoints

### Authentication
- `POST /api/Auth/login` - Admin login
- `POST /api/Auth/register` - Register new admin
- `GET /api/Auth/me` - Get current user info

### Bookings
- `GET /api/Bookings` - Get all bookings (Auth required)
- `GET /api/Bookings/{id}` - Get booking by ID (Auth required)
- `POST /api/Bookings` - Create new booking
- `PUT /api/Bookings/{id}` - Update booking (Auth required)
- `DELETE /api/Bookings/{id}` - Delete booking (Auth required)
- `POST /api/Bookings/available` - Check table availability

### Tables
- `GET /api/Tables` - Get all tables
- `GET /api/Tables/{id}` - Get table by ID
- `POST /api/Tables` - Create table (Auth required)
- `PUT /api/Tables/{id}` - Update table (Auth required)
- `DELETE /api/Tables/{id}` - Delete table (Auth required)

### Menu
- `GET /api/Menu` - Get all menu items
- `GET /api/Menu/{id}` - Get menu item by ID
- `GET /api/Menu/popular` - Get popular items
- `POST /api/Menu` - Create menu item (Auth required)
- `PUT /api/Menu/{id}` - Update menu item (Auth required)
- `DELETE /api/Menu/{id}` - Delete menu item (Auth required)

### Customers
- `GET /api/Customers` - Get all customers (Auth required)
- `GET /api/Customers/{id}` - Get customer by ID (Auth required)
- `POST /api/Customers` - Create customer (Auth required)
- `PUT /api/Customers/{id}` - Update customer (Auth required)
- `DELETE /api/Customers/{id}` - Delete customer (Auth required)

## Default Admin Account

After running the application, you can login with:
- **Username**: `admin`
- **Password**: `Admin1234!`

## Project Structure

```
├── Controllers/         # API Controllers
├── DTOs/               # Data Transfer Objects
├── Models/             # Entity Models
├── Data/               # Database Context
├── Services/           # Business Logic Services
├── Repositories/       # Data Access Layer
├── Extensions/         # Service Extensions
└── Migrations/         # EF Core Migrations
```

## Architecture Features

- **Repository Pattern**: Clean separation of data access logic
- **Service Layer**: Business logic encapsulation
- **JWT Authentication**: Secure token-based auth
- **Extension Methods**: Clean dependency injection setup
- **CORS Enabled**: Ready for frontend integration
- **Swagger Integration**: Automatic API documentation

## Database Schema

The API manages the following entities:
- **Administrators**: Admin user accounts
- **Customers**: Customer information
- **Tables**: Restaurant table configuration
- **Bookings**: Table reservations
- **MenuItems**: Restaurant menu items

## Security Features

- JWT token authentication for admin operations
- Password hashing using BCrypt
- CORS policy for frontend integration
- Input validation on all endpoints
- Secure connection string management

## Frontend Integration

This API is designed to work with the [Timo's Trattoria MVC Frontend](https://github.com/timsuv/TimosTrattoria). Configure the frontend to connect to:
- API Base URL: `https://localhost:7189/api/`

## Development Notes

- Database is created automatically on first run
- Seed data includes realistic Italian restaurant menu
- All timestamps use UTC
- Booking duration defaults to 2 hours
- Table availability accounts for overlapping time slots

## Troubleshooting

**Database Connection Issues:**
- Ensure SQL Server is running
- Check connection string format
- Verify database permissions

**JWT Authentication Issues:**
- Ensure SecretKey is at least 32 characters
- Check token expiration (24 hours default)
- Verify Issuer/Audience configuration

**Port Conflicts:**
- Default ports: 7189 (HTTPS), 5013 (HTTP)
- Update in `launchSettings.json` if needed

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is for educational/demonstration purposes.
