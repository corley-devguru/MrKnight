# Knight's Shortest Path API

## Introduction

This application provides an API to solve the shortest path for a knight in a chess game from a given start position to an end position on a standard 8x8 chessboard. The solution consists of three serverless functions: one to create the request, one to process it, and one to return the results.

## Architecture

The project follows Clean Architecture principles and consists of the following layers:

- **Domain**: Contains the core business logic and entities.
- **Application**: Contains the services and interfaces for use cases.
- **Infrastructure**: Contains data access implementations using Entity Framework Core.
- **Presentation**: Contains the serverless functions and API endpoints.

## Requirements

- .NET 6 SDK
- Azure Functions Core Tools (for local development)
- Visual Studio 2022 or Visual Studio Code

## Setup

- An in-memory database is used, so there is no need to run migrations.
- The project is set up using .NET 6.0.

## Testing Endpoints

- Inside the `MrKnight.API` project, there is an `endpoints.http` file. This file can be used to test the endpoints, providing an easier alternative to Swagger and Postman.

## Design Choice

- When a path from a source to a target has already been computed, it is not recomputed. Instead, the existing `operationId` is returned.

