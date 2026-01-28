# Architecture

This repository follows a layered architecture:

- **Core**
  - Domain: entities, value objects, and business rules.
  - Application: use cases and ports (interfaces) that define required behaviors.
- **Infrastructure**
  - Implementations of ports (e.g., persistence, clocks, external services).
- **API**
  - Minimal API endpoints, composition root, and DI setup.

## Dependency rule
Core must not depend on Infrastructure or API. All dependencies point inward:

```
API -> Infrastructure -> Core
```

This keeps business logic stable and testable.