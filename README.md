# 🛒 ECommerceWeb API (.NET 8) — Onion Architecture

A scalable and modular **ECommerce backend** built with ASP.NET Core 8 using **Onion Architecture**.  
The project demonstrates a clean separation of concerns with robust features such as JWT authentication, Redis-based basket, Stripe payment integration, and advanced patterns.

---

## 🧱 Architecture Overview

This solution follows **Onion Architecture**, with the project separated into 7 Projects:

![Solution Structure](assets/solution-structure.png)

> Includes Core, Infrastructure, Web, and Shared layers with clear boundaries and dependency flow.

---

## 🚀 Features

- 🔐 JWT Authentication (with roles)
- 🧺 Basket Module using Redis
- 💳 Stripe Integration for Payments
- 🗂️ Repository + Unit of Work Pattern
- 🧠 Specification Pattern for flexible querying
- 🔁 AutoMapper & Manual Mapping
- 🛠️ Dependency Injection via `ServiceRegistration`
- 📃 Pagination & Filtering for API resources
- 🧵 Custom Exception Middleware
- 📑 Swagger/OpenAPI documentation
- 🧪 Ready for Unit and Integration Testing

---

## 🗂️ Projects Breakdown

| Project                     | Description                                              |
|----------------------------|----------------------------------------------------------|
| `Core/Domain`              | Domain entities and enums                                |
| `Core/Services`            | Business logic implementations                           |
| `Core/ServicesAbstractions`| Interfaces for services                                  |
| `Infrastructure/Persistence` | Migrations,DataSeeding, Repositories                   |
| `Infrastructure/Presentation` | View models, Controllers                              |
| `ECommerce.Web`            | Web API: endpoints, middleware, Swagger, startup config  |
| `Shared`                   | Shared utilities/Dtos                                    |

---

## 🧪 Technologies Used

- **ASP.NET Core 8**
- **Entity Framework Core**
- **Redis**
- **Stripe SDK**
- **AutoMapper**
- **JWT Tokens**
- **xUnit + Moq**
- **Swagger (NSwag/Swashbuckle)**
- **Clean Onion Architecture**
