# StudentApi

> A clean **ASP.NET Core Web API** (2025) for managing student records, built with **Entity Framework Core**, **AutoMapper**, and **FluentValidation** following a layered Controller → Service → Repository architecture.

---

## Overview

StudentApi is a RESTful CRUD API for `Student` records. It demonstrates a maintainable ASP.NET Core project structure:

- **Controller layer** handles HTTP requests/responses.
- **Service layer** holds business logic and DTO mapping.
- **Repository layer** encapsulates data access via EF Core.
- **DTOs + AutoMapper** decouple the API contract from the database entity.
- **FluentValidation** validates incoming data before it reaches the database.
- **Swagger / OpenAPI** provides interactive API documentation.

The data is persisted in a local **SQLite** database via EF Core with code-first migrations.

## Tech Stack

- **.NET 10** Web API, C#
- **Entity Framework Core** (SQLite provider, code-first migrations)
- **AutoMapper** — entity ↔ DTO mapping
- **FluentValidation** — request validation
- **Swagger / Swashbuckle** (OpenAPI) — API docs
- Repository pattern + service layer

## Data Model

`Student`:

| Field | Type | Notes |
|---|---|---|
| `Id` | int | Primary key (auto-generated) |
| `Name` | string | Required |
| `Age` | int | — |
| `Email` | string | Required, valid email format |

**Validation rules (FluentValidation):**

- `Name` — required, minimum 2 characters
- `Email` — required, valid email format
- `Age` — between 18 and 60 (inclusive)

## API Endpoints

Base route: `/api/students`

| Method | Route | Description | Success response |
|---|---|---|---|
| `GET` | `/api/students` | Get all students | `200 OK` — list of students |
| `GET` | `/api/students/{id}` | Get a student by id | `200 OK` / `404 Not Found` |
| `POST` | `/api/students` | Create a new student | `201 Created` (with `Location` header) |
| `PUT` | `/api/students/{id}` | Update an existing student | `204 No Content` / `404` / `400` on id mismatch |
| `DELETE` | `/api/students/{id}` | Delete a student | `204 No Content` / `404 Not Found` |

**Example request body (`POST` / `PUT`):**

```json
{
  "id": 0,
  "name": "Jane Doe",
  "age": 22,
  "email": "jane@example.com"
}
```

## How to Run

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
# 1. Clone
git clone https://github.com/krishna-chandra-dolai/StudentApi.git
cd StudentApi

# 2. Apply EF Core migrations (creates the SQLite database)
dotnet ef database update

# 3. Run
dotnet run
```

Then open the **Swagger UI** at the URL printed in the console (e.g. `https://localhost:<port>/swagger`) to explore and test the endpoints. A simple JavaScript demo is also available at `/test.html`.

> If the `dotnet ef` command is not found, install the tool with `dotnet tool install --global dotnet-ef`. The connection string is configured in `appsettings.json` (`Data Source=studentdb.sqlite`).

## Project Structure

```text
StudentApi/
├── Controllers/         # StudentsController — API endpoints
├── Services/            # IStudentService / StudentService — business logic
├── Repositories/        # IStudentRepository / StudentRepository — EF Core data access
├── Models/              # Student entity + FluentValidation validators
├── Dtos/                # StudentDto — API contract
├── Profiles/            # AutoMapper mapping profile
├── Data/                # AppDbContext (EF Core)
├── Migrations/          # EF Core code-first migrations
├── Images/              # Swagger screenshots
├── wwwroot/             # test.html demo page
├── Program.cs           # DI, middleware, and app configuration
└── appsettings.json
```

## Swagger Screenshots

| | |
|---|---|
| ![Swagger Interface](Images/INTERFACE.png) | ![GET Students](Images/GET.png) |
| ![POST Student](Images/POST%20ID.png) | ![GET by ID](Images/GET%20ID.png) |
| ![PUT Student](Images/PUT%20ID.png) | ![DELETE Student](Images/DELETE%20ID.png) |

## License

Released under the [MIT License](LICENSE).

## Author

**Krishna Chandra Dolai** — [LinkedIn](https://linkedin.com/in/krishna-chandra-dolai)
