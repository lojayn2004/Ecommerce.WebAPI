# Ecommerce API

This project is a simple e-commerce backend built with ASP.NET Core during a course on building real-world APIs. It is meant to be a practical reference for learning how a full online store backend is structured, including authentication, product catalog management, basket logic, order processing, and payment integration.

## Project purpose

The API includes the main pieces of an ecommerce system:

- Product listing and filtering
- Product brands and types endpoints
- User registration and login
- JWT-based authentication
- Shopping basket management
- Order creation and retrieval
- Delivery methods
- Stripe payment intent flow and webhook handling
- Redis-backed caching
- PostgreSQL data storage


## Tech stack

- ASP.NET Core Web API
- C#
- PostgreSQL
- Redis
- Entity Framework Core
- ASP.NET Core Identity
- JWT authentication
- Stripe API
- AutoMapper
- Swagger / OpenAPI




## Local configuration

The API configuration is stored in:

- Ecommerce_api/appsettings.json
- Ecommerce_api/appsettings.Development.json

Update the connection strings and secrets for your local environment if needed, especially:

- ConnectionStrings:DefaultConnection
- ConnectionStrings:IdentityDbConnection
- ConnectionStrings:RedisConnection
- JWT settings
- Stripe keys

```json 
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "",
    "RedisConnection": "",
    "IdentityDbConnection": ""
  },
  "JWT": {
    "key": "",
    "Issuer": "",
    "Audience": ""

  },
  "Stripe": {
    "SecretKey": "",
    "DefaultCurrency": "",
    "WebhookSecret": ""
  },
  "UrlSettings": {
    "BaseUrl": "http://localhost:5084"
  }
}
```

## Run the project

From the project root, run:

```bash
dotnet restore

dotnet build

dotnet run --project Ecommerce_api/Ecommerce.api.csproj
```

Then open the Swagger UI in the browser:

```text
https://localhost:7091/swagger
```



## Important API flows

### Auth

- POST /api/Auth/register
- POST /api/Auth/login

### Products

- GET /Products
- GET /Products/{id}
- GET /Products/brands
- GET /Products/types

### Basket

- GET /Basket/{id}
- POST /Basket
- DELETE /Basket

### Orders

- POST /api/Orders
- GET /api/Orders
- GET /api/Orders/{orderId}
- GET /api/Orders/delivery-methods

### Payment

- POST /api/Payment/{basketId}
- POST /api/Payment/webhook

