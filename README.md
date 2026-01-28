# Cobracco Architecture Example

Minimal, production-grade sample showing a clean layering approach with a .NET 9 Minimal API backend and a Vite React TypeScript frontend. The Core (Domain + Application) is isolated from Infrastructure and API.

## Stack
- Backend: .NET 9 Minimal API
- Frontend: Vite + React + TypeScript
- Tests: xUnit

## Architecture rules
- Core (Domain + Application) must not depend on Infrastructure or API.
- Infrastructure depends on Core.
- API depends on Core + Infrastructure.
- Tests reference Core only.

## Run instructions
### Backend
From `backend/`:

```
DOTNET_ENVIRONMENT=Development dotnet run --project src/Cobracco.ArchExample.Api --urls http://localhost:5000
```

### Frontend
From `frontend/cobracco-arch-example-web/`:

```
npm install
npm run dev
```

The Vite dev server proxies `/api` to `http://localhost:5000`.

## Endpoints
- `GET /api/notes` – list notes
- `POST /api/notes` – create note

- ## Demo UI
![Notes demo](docs/images/ui.png)

## Links
- https://www.cobracco.it
- https://www.linkedin.com/company/cobracco
