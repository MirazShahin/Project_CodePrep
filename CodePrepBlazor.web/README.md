# CodePrep Blazor Frontend

.NET 9 **Blazor Web App** (Interactive Server) frontend for the CodePrep / DevPrep API.

## Features

- **Auth** – Login / Register with JWT stored in protected local storage
- **Dashboard** – Progress stats & quick actions
- **Topics** – Browse / search by category, topic detail + resources
- **Problems** – Filterable problem list & detail
- **Codeforces** – Live problems via API (tag + rating filters)
- **Learning Path** – Curated content with modal reader
- **Interview Prep** – Expandable Q&A bank
- **AI Tutor** – Roadmap & quiz generation
- **CP Assistant** – Code explain + free-form AI ask
- **Theme** – Light / soft-dark hybrid (toggle in top bar)

## Run

1. Start the **CodePrep.API** backend (default `https://localhost:7001`).
2. Update `appsettings.json` → `ApiBaseUrl` if needed.
3. Run this project:

```bash
cd CodePrepBlazor
dotnet restore
dotnet run
```

Open `http://localhost:5200` (or the HTTPS URL from launchSettings).

## CORS

Ensure the API allows the Blazor origin. In `Program.cs` of the API, the CORS policy should include e.g. `http://localhost:5200` and `https://localhost:7200`.

## Project structure

```
CodePrepBlazor/
  Components/
    Layout/MainLayout.razor
    Pages/          # all routes
    Shared/         # spinner, empty state, badges, toast
  Models/           # DTOs matching API
  Services/         # AuthService, ApiClient, Theme, Toast
  wwwroot/css/app.css
```

## Theme

CSS variables under `[data-theme="light"]` and `[data-theme="dark"]`. Default is light; users can switch with the sun/moon button. Preference is persisted.
