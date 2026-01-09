# ArticleManagementApp

A modern, cloud-native article management platform built with .NET 10, Blazor Server, MongoDB, and Bootstrap.

## 📋 Project Overview

ArticleManagementApp is a greenfield article/blog management system designed for teams to create, edit, and publish articles with role-based access control. The application uses modern cloud-native architecture patterns and provides a responsive Bootstrap-based UI with dark mode support.

### Key Features

- 📝 **Article Management**: Create, edit, archive, and publish articles with markdown support
- 🏷️ **Category Organization**: Organize articles by categories
- 🔐 **Role-Based Access Control**: Admin, Author, and User roles via Auth0
- 🌙 **Dark Mode**: Built-in theme toggle with localStorage persistence
- 📱 **Responsive Design**: Bootstrap 5.3 with mobile-first approach
- 🗄️ **Cloud-Native**: .NET Aspire orchestration with MongoDB and Redis
- ✅ **Comprehensive Testing**: Unit, integration, and architecture tests (no E2E)

## 🛠️ Tech Stack

- **.NET 10** with C# 13
- **Blazor Server** for interactive SSR UI
- **MongoDB 8.0** for document storage
- **.NET Aspire** for cloud-native orchestration
- **Redis** for caching and output cache
- **Auth0** for OAuth authentication
- **Bootstrap 5.3** for responsive styling (via jsDelivr CDN)
- **xUnit + bUnit + TestContainers** for testing

## 🏗️ Architecture

- **CQRS Pattern**: Separate command and query handlers
- **Vertical Slice Architecture**: Features are self-contained
- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Built-in .NET DI container
- **Result<T> Pattern**: Consistent error handling

## 📦 Project Structure

```text
ArticleManagementApp/
├── src/
│   ├── AppHost/                 # .NET Aspire orchestration
│   ├── Web/                     # Blazor Server application
│   ├── Shared/                  # Domain models and abstractions
│   └── ServiceDefaults/         # Shared service configuration
└── tests/
    ├── Shared.Tests.Unit/       # Domain model tests
    ├── Web.Tests.Unit/          # Component and service tests
    ├── Web.Tests.Integration/   # Integration tests with TestContainers
    └── Architecture.Tests/      # Code structure validation
```

### Global usings

Each project centralizes common namespaces to reduce per-file noise (see `GlobalUsings.cs`):

- `src/AppHost/GlobalUsings.cs` – Aspire hosting primitives.
- `src/Web/GlobalUsings.cs` – Blazor, routing, DI, logging, and HttpClient helpers.
- `src/Shared/GlobalUsings.cs` – core system and validation primitives.
- `src/ServiceDefaults/GlobalUsings.cs` – configuration and hosting helpers.
- Tests: `tests/*/GlobalUsings.cs` – shared test imports (xUnit).

Example stub:

```csharp
// src/Web/GlobalUsings.cs
global using System.Net.Http.Json;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Components.Routing;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
```

### Sample stubs

Start new feature slices with these minimal stubs:

```csharp
// src/Shared/Entities/Article.cs
namespace ArticleManagementApp.Shared.Entities;

public sealed class Article
{
   public Guid Id { get; init; }
   public string Title { get; set; } = string.Empty;
   public string Slug { get; set; } = string.Empty;
   public string Body { get; set; } = string.Empty;
   public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}
```

```razor
@* src/Web/Components/Articles/ArticleList.razor *@
@using ArticleManagementApp.Shared.Entities

<h2 class="h4 mb-3">Articles</h2>
<ul class="list-group">
   <li class="list-group-item" aria-live="polite">Article list placeholder</li>
</ul>
```

## 🚀 Getting Started

### Prerequisites

- .NET 10 SDK
- Docker Desktop (for MongoDB, Redis, TestContainers)
- Visual Studio Code, Visual Studio 2026, or JetBrains Rider
- Git

### Local Development Setup

1. **Clone the repository**:

   ```bash
   git clone https://github.com/yourusername/ArticleManagementApp.git
   cd ArticleManagementApp
   ```

2. **Restore NuGet packages**:

   ```bash
   dotnet restore
   ```

3. **Configure Auth0** (see [SETUP.md](./SETUP.md)):

   ```bash
   cp appsettings.Development.json.template appsettings.Development.json
   # Edit appsettings.Development.json with your Auth0 credentials
   ```

4. **Run with Aspire** (starts MongoDB, Redis, and Web app):

   ```bash
   dotnet watch run --project src/AppHost
   ```

5. **Open browser**:

   ```text
   http://localhost:5000
   ```

### Running Tests

```bash
# All tests
dotnet test

# Unit tests only
dotnet test tests/Shared.Tests.Unit tests/Web.Tests.Unit

# Integration tests (requires Docker)
dotnet test tests/Web.Tests.Integration

# With code coverage
dotnet test --collect:"XPlat Code Coverage"
```

## 🔐 Authentication & Authorization

The application uses **Auth0** for authentication with three roles:

- **Admin**: Full access to all features, user management, admin dashboard
- **Author**: Create and edit their own articles, view all published content
- **User**: Read-only access to published articles

See [SETUP.md](./SETUP.md) for Auth0 configuration steps.

## 🗂️ Folder Structure Overview

- **Components/Layout/**: Main layout, navbar, footer, theme toggle
- **Components/Shared/**: Reusable UI components (alerts, loading, page headers)
- **Components/Articles/**: Article list, details, form, and CQRS handlers
- **Components/Categories/**: Category management components and handlers
- **Pages/**: Page-level Razor components (Home, Articles, Admin, etc.)
- **Data/**: MongoDB context and repository implementations
- **Services/**: Business logic services (auth, file upload, seeding)
- **Shared/Entities/**: Domain models (Article, Category)
- **Shared/Models/**: DTOs and API contracts
- **Shared/Abstractions/**: Interfaces and base patterns (Result&lt;T&gt;)
- **Shared/Validators/**: FluentValidation rules

## 📚 Documentation

- **[SETUP.md](./SETUP.md)**: Step-by-step Auth0 and local environment configuration
- **[ARCHITECTURE.md](./docs/ARCHITECTURE.md)**: Detailed architecture decisions and patterns
- **[CONTRIBUTING.md](./docs/CONTRIBUTING.md)**: Contribution guidelines

## 🐳 Docker & Deployment

### Local Docker Compose

```bash
docker-compose up -d
```

### Building the Docker Image

```bash
docker build -t articlemgmt:latest .
```

## 📈 Development Roadmap

**Phase 1 (Current)**: MVP with Articles, Categories, basic Admin dashboard

**Phase 2**:

- Full-text search
- Comments and reactions
- Tag support
- Analytics dashboard

**Phase 3**:

- REST API layer
- Mobile app support
- Advanced scheduling
- Content versioning

## 🤝 Contributing

See [CONTRIBUTING.md](./docs/CONTRIBUTING.md) for guidelines.

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👨‍💻 Author

Built with ❤️ by the ArticleManagementApp team.

---

## Quick Links

- 📖 [Setup Guide](./SETUP.md)
- 🏗️ [Architecture Documentation](./docs/ARCHITECTURE.md)
- ✅ [Code Style Guidelines](./docs/CONTRIBUTING.md)
- 🐛 [Issues & Feature Requests](https://github.com/yourusername/ArticleManagementApp/issues)
