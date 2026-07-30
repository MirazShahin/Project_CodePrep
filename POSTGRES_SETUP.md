# CodePrep – PostgreSQL Setup

## 1. Local Postgres (Docker)

```bash
docker compose up -d postgres
```

Default connection (already in `appsettings.json`):

```
Host=localhost;Port=5432;Database=CodePrepDb;Username=postgres;Password=postgres
```

## 2. Packages (already updated)

- `Npgsql.EntityFrameworkCore.PostgreSQL` (v9.0.4)
- Removed `Microsoft.EntityFrameworkCore.SqlServer`

## 3. Apply migrations

From solution root:

```bash
dotnet ef database update \
  --project CodePrep.Infrastructure \
  --startup-project CodePrep.API
```

Or just run the API – `Program.cs` calls `Database.Migrate()` on startup.

## 4. New migration (if model changes)

```bash
dotnet ef migrations add YourMigrationName \
  --project CodePrep.Infrastructure \
  --startup-project CodePrep.API \
  --output-dir Migrations
```

## 5. Production (e.g. Render / Railway / Neon)

Set environment variable (overrides appsettings):

```
ConnectionStrings__DefaultConnection=Host=YOUR_HOST;Port=5432;Database=YOUR_DB;Username=YOUR_USER;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true
```

## 6. Connection string format (Npgsql)

| SQL Server (old) | PostgreSQL (new) |
|------------------|------------------|
| `Server=...;Database=...;Trusted_Connection=True;` | `Host=...;Port=5432;Database=...;Username=...;Password=...` |

Do **not** use `Trusted_Connection` or `TrustServerCertificate` SQL Server style with Npgsql.
Use `SSL Mode=Require` when the host requires SSL.
