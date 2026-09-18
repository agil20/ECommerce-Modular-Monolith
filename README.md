# ECommerce — Modular Monolith (.NET 10)

A modular monolith e-commerce backend built with ASP.NET Core, EF Core and MassTransit/RabbitMQ.
Each business capability lives in its own module with its own database schema, and modules talk to
each other through contracts and asynchronous messages instead of direct database access.

## Architecture

The solution is a **modular monolith**: one deployable application, several independent modules.

```
ECommerceApi                 → host (composition root, Swagger, auth, pipeline)
Common                       → shared kernel (BaseEntity, exceptions, ApiResponseModel, repositories)

Modules.Categories.*         → Domain / Application / Contracts / Infrastructure / Presentation
Modules.Products.*           → Domain / Application / Contracts / Infrastructure / Presentation
Modules.Baskets.*            → Domain / Application / Contract  / Infrastructure / Presentation
Modules.Identity.*           → Domain / Contracts / Infrastructure / Presentation
```

Layer responsibilities inside a module:

| Layer | Contains |
|---|---|
| `Domain` | Entities, no framework dependencies |
| `Application` | Repository interfaces, validators, use-case services |
| `Contracts` | DTOs, module service interfaces, integration messages — the only part other modules may reference |
| `Infrastructure` | EF Core `DbContext`, repositories, service implementations, consumers, DI extension |
| `Presentation` | API controllers |

**Module isolation rules**

- Each module owns a separate `DbContext` and a separate PostgreSQL schema (`Categories`, `Products`, `Baskets`, `Identity`).
- No cross-module navigation properties or foreign keys — modules reference each other by id only
  (for example `Product.CategoryId`, `Basket.UserId`).
- Cross-module communication goes through a module's `Contracts` interface (in-process) or through
  RabbitMQ events (asynchronous).

## Tech stack

- .NET 10 / ASP.NET Core
- Entity Framework Core 10 + PostgreSQL (Npgsql)
- MassTransit 8.5.10 + RabbitMQ
- ASP.NET Core Identity + JWT bearer authentication
- Swagger (separate document per module)

## Messaging patterns

Implemented with MassTransit over RabbitMQ:

- **Publish/Subscribe** — `CategoryDeletedEvent` is published by the Categories module and consumed by
  the Products module, which soft-deletes the affected products. `ProductPriceChangedEvent` is
  consumed to write price-history rows.
- **Transactional Outbox** — the Categories module stores outgoing messages in the same database
  transaction as the entity change, so a message can never be published for a transaction that
  rolled back (and vice versa).
- **Retry and error queues** — failed messages are retried three times; if they still fail they are
  moved to a `<queue>_error` queue together with the exception type, message, retry count and stack
  trace, instead of being silently lost.

## Cross-cutting concerns

- **Soft delete** — `SaveChangesAsync` is overridden in each `DbContext` to convert `Deleted` entities
  into `IsDeleted = true` updates, combined with a global query filter so deleted rows disappear from
  all queries automatically.
- **Global exception handling** — `IExceptionHandler` implementation maps domain exceptions
  (`NotFoundException`, `DublicatedDataException`, `ConfilictException`) to problem details responses.
- **Uniform API responses** — all endpoints return `ApiResponseModel` (`isSuccess`, `statusCode`, `message`, `data`).

## Authentication

ASP.NET Core Identity stores users in the `Identity` schema; login issues a signed JWT.

| Endpoint | Description |
|---|---|
| `POST /api/Auth/register` | Creates a user and returns a JWT |
| `POST /api/Auth/login` | Validates credentials and returns a JWT |

The token carries `sub`, `email`, `jti` and `nameidentifier` claims plus the user's roles and
permissions, and is validated against issuer, audience, lifetime and signing key.

The basket endpoints require a token and never take a basket id from the client: the user id is read
from the token, so a user can only ever reach their own basket.

## Authorization (permission-based)

Endpoints are protected by **permissions**, not by role names. Roles are just groups of permissions.

```csharp
[HttpPost]
[HasPermission(Permissions.Categories.Create)]
public async Task<IActionResult> Post(RequestCategoryCreate dto) { ... }
```

- Permissions are defined once in `Common/Authorization/Permissions.cs`
  (`categories.create|update|delete`, `products.create|update|delete`).
- Each role's permissions are stored as role claims in Identity's `AspNetRoleClaims` table — no extra
  tables or migrations.
- At login every permission from the user's roles is written into the JWT as a `permission` claim.
- Every permission is registered as an authorization policy of the same name; `[HasPermission(x)]` is
  `[Authorize(Policy = x)]`.

| Role | Permissions |
|---|---|
| `Admin` | all category and product write permissions |
| `User` | none — can manage only their own basket; assigned automatically on register |

Read endpoints (`GET`) for categories and products are public. Write endpoints return `401` without a
token and `403` when the token lacks the permission.

Roles, the admin role's permissions and an admin account are seeded at startup (`IdentitySeeder`).
Because permissions live in the token, a change to a role's permissions takes effect at the user's
next login.

## Getting started

### Prerequisites

- .NET 10 SDK
- PostgreSQL
- A RabbitMQ instance (local or hosted, e.g. CloudAMQP)

### Configuration

`ECommerceApi/appsettings.json` is committed with placeholder values only. Put real values in
`ECommerceApi/appsettings.Development.json` (git-ignored) or in user secrets:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=YOUR_DATABASE;Username=YOUR_DB_USER;Password=YOUR_DB_PASSWORD"
  },
  "RabbitMQ": {
    "Host": "YOUR_RABBITMQ_HOST",
    "VirtualHost": "YOUR_RABBITMQ_VHOST",
    "Username": "YOUR_RABBITMQ_USER",
    "Password": "YOUR_RABBITMQ_PASSWORD"
  },
  "Jwt": {
    "Key": "YOUR_JWT_SECRET_KEY_AT_LEAST_32_CHARACTERS_LONG",
    "Issuer": "ECommerceApi",
    "Audience": "ECommerceApiUsers",
    "ExpireMinutes": 60
  },
  "AdminUser": {
    "Email": "YOUR_ADMIN_EMAIL",
    "Password": "YOUR_ADMIN_PASSWORD"
  }
}
```

The JWT key must be at least 32 characters, otherwise HMAC-SHA256 signing fails at runtime.
`AdminUser` is optional: when present, an account with the `Admin` role is created on startup.

### Database

Each module has its own `DbContext` and migration history, so migrations are applied per context:

```bash
dotnet ef database update --project Modules.Categories.Infrastructure --startup-project ECommerceApi --context CategoriesDbContext
dotnet ef database update --project Modules.Products.Infrastructure   --startup-project ECommerceApi --context ProductsDbContext
dotnet ef database update --project Modules.Baskets.Infrastructure    --startup-project ECommerceApi --context BasketDbContext
dotnet ef database update --project Modules.Identity.Infrastructure   --startup-project ECommerceApi --context IdentityModuleDbContext
```

### Run

```bash
dotnet run --project ECommerceApi
```

Swagger UI is served at the application root in development, with one document per module
(categories, products, baskets, identity).
