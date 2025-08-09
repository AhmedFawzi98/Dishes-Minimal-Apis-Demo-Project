This project is a demo for learning how to structure and organize, and extend Minimal APIs using modern techniques, with a focus on using vertical slicing architecture, Exception Handling, endpoint filters, API versioning, .NET9 swashbuckle alternative for OpenAPI documentation generating, and Swagger UI alternative (Scalar UI).
demo purpose is to explore Minimal API capabilities and differences in implementing functionalities VS. MVC based APIs.

---

## 📜 Project Brief
a .NET 9 Minimal APIs built around simple CRUD for managing **"dishes"**, demonstrating:

- Organization techniques for Minimal APIs and dynamic endpoint registration/mapping
- Endpoint filters for cross-cutting concerns:
  - API key validation
  - Logging
  - Request timing
- Exception handling and custom exception and unified error response. (RFC compliant)
- Route grouping & API version sets
- API versioning using URL segments
- Documentation with **OpenAPI** & **Scalar UI**:
  - Fully documented endpoints with summaries, descriptions, and response types

---

## ⚠ Exception Handling
The project uses **custom middleware** for exception handling:
- A `GlobalExceptionHandler` processes unhandled exceptions
- Custom exception types are used to produce **RFC-compliant** error responses:
  - `ProblemDetails` for general errors
  - `ValidationProblemDetails` for validation failures
- This ensures standardized, descriptive, and machine-readable error formats

---

## 🛠 Data Access
A **simple repository pattern** is used.  
Since this is a demo project, common data access patterns are simplified/omitted:
- No filtering, sorting, or pagination
- No Unit of Work or transactional handling — just a basic repository
- No external mapping library (e.g., AutoMapper) — repositories handle mapping to DTOs for simplicity

---

## 🗄 Background Service for Database Seeding
On startup, a hosted service seeds the database with initial data for demonstration purposes.

---

## 🔑 API-Key Requirement
Every request requires an API key in the header:
Api-Key: "L7TxLl9ZPMHWCBgdrzg0ysaYJ5LSfKwB"
*(Configured in `appsettings.json`, purely for demo purposes.)*

---

## 📄 API Documentation
Scalar UI URL:{{baseAddress}}/docs/{{version}}
Examples:
- v1 → [https://localhost:7124/docs/v1] (https://localhost:7124/docs/v1)  
- v2 → [https://localhost:7124/docs/v2] (https://localhost:7124/docs/v2)

---


