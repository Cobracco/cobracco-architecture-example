# ADR-0001: Layered Architecture

## Status
Accepted

## Context
We need a simple, maintainable structure that keeps business logic independent from infrastructure and delivery concerns.

## Decision
Adopt a layered architecture with a Core (Domain + Application), Infrastructure, and API. Core is isolated from Infrastructure and API.

## Consequences
- Business logic is testable and reusable.
- Infrastructure can be swapped without touching Core.
- API acts as composition root and boundary for transport concerns.