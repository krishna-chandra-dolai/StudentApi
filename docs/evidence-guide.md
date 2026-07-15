# Project Evidence Guide

## Automated evidence

- GitHub Actions builds and tests the project on .NET 10.
- `StudentApi.Tests` covers successful create-and-retrieve behavior, invalid request validation, and missing-resource responses.

## API screenshots

| Evidence | File |
|---|---|
| Swagger interface | [`Images/INTERFACE.png`](../Images/INTERFACE.png) |
| GET all students | [`Images/GET.png`](../Images/GET.png) |
| GET by ID | [`Images/GET ID.png`](../Images/GET%20ID.png) |
| POST student | [`Images/POST ID.png`](../Images/POST%20ID.png) |
| PUT student | [`Images/PUT ID.png`](../Images/PUT%20ID.png) |
| DELETE student | [`Images/DELETE ID.png`](../Images/DELETE%20ID.png) |

## Implementation evidence

- `Migrations/20251211141238_InitialCreate.cs` records the EF Core code-first schema migration.
- `wwwroot/test.html` is a browser demo served at `/test.html`.
- `Controllers`, `Services`, and `Repositories` show the layered request-to-data flow.

The screenshots are historical UI evidence. CI and integration tests are the current executable evidence.
