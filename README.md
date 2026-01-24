# 🌾 AgriProcurement Platform - Agricultural Procurement System

## 📌 Project Overview

AgriProcurement is a modern microservices-based platform designed for agricultural product procurement. The project is built using .NET 9 and ASP.NET Core following Clean Architecture principles.

## 🏗️ System Architecture

![System Architecture](image.png)

The system consists of the following core components:
- **API Gateway** - Routes all requests
- **Procurement Service** - Manages procurement operations
- **Supplier Service** - Manages supplier operations
- **RabbitMQ** - Asynchronous message exchange
- **PostgreSQL** - Database

## 🛠️ Technology Stack

### Backend
- **.NET 9** - Core framework
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM
- **MediatR** - CQRS pattern
- **RabbitMQ** - Message broker
- **PostgreSQL** - Database
- **Serilog** - Logging
- **OpenTelemetry** - Monitoring
- **Prometheus** - Metrics collection

### DevOps & Infrastructure
- **Docker** - Containerization
- **Kubernetes** - Orchestration
- **Helm** - Kubernetes package manager
- **ArgoCD** - GitOps deployment
- **Grafana** - Monitoring dashboard
- **Loki** - Log aggregation

## 📁 Project Structure

```
AgriProcurement/
├── src/
│   ├── ApiGateway/                    # API Gateway
│   ├── BuildingBlocks/                # Shared libraries
│   └── Modules/
│       ├── Procurement/               # Procurement module
│       │   ├── Domain/               # Domain layer
│       │   ├── Application/          # Application layer
│       │   ├── Infrastructure/       # Infrastructure layer
│       │   └── API/                  # Presentation layer
│       └── Supplier/                  # Supplier module
│           ├── Domain/
│           ├── Application/
│           ├── Infrastructure/
│           └── API/
├── deploy/
│   └── helm/                         # Helm charts
└── tests/                            # Test projects
```

## 🚀 Deployment Workflow

1. **Application** is packaged using Helm
2. **Helm charts** are stored in Git repository
3. **ArgoCD** watches the repository for changes
4. **Kubernetes resources** are synced automatically
5. **Application pods** are deployed to the cluster
6. **Prometheus** scrapes metrics from /metrics endpoint
7. **Grafana** visualizes metrics and logs
8. **Loki** collects logs via Promtail

## 🚀 Getting Started

### Prerequisites
- .NET 9 SDK
- Docker Desktop
- PostgreSQL
- RabbitMQ

### Local Development Setup

1. **Clone the repository:**
```bash
git clone <repository-url>
cd AgriProcurement
```

2. **Setup database:**
```bash
# Start PostgreSQL
docker run --name postgres -e POSTGRES_PASSWORD=password -p 5432:5432 -d postgres

# Apply migrations
dotnet ef database update --project AgriProcurement.Procurement.Infrastructure
```

3. **Start RabbitMQ:**
```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

4. **Run the applications:**
```bash
# Procurement API
dotnet run --project AgriProcurement.Procurement.API

# Supplier API
dotnet run --project AgriProcurement.Supplier.API

# API Gateway
dotnet run --project ApiGateway
```

## 📊 Monitoring & Logging

### Prometheus Metrics
![Prometheus Monitoring](image-1.png)

**Collected Metrics:**
- HTTP request duration
- Request count by status code
- CPU and memory usage
- .NET GC metrics
- Thread count

### Grafana Dashboard
![Grafana Dashboard](image-2.png)

**Available Dashboards:**
- Kubernetes Cluster Overview
- Application Metrics Dashboard
- Node Exporter Metrics

### Loki Logging
![Loki Logging](image-3.png)

**Logging Features:**
- Logs collected from all application pods
- Centralized log storage using Loki
- Log visualization in Grafana
- Namespace filtering
- Pod and container filtering
- Log level search (INFO/WARN/ERROR)

## 🔄 GitOps & Deployment

### ArgoCD
![ArgoCD GitOps](image-4.png)

**ArgoCD Responsibilities:**
- Continuous synchronization between Git and cluster
- Declarative Kubernetes deployments
- Drift detection and self-healing
- Rollback using Git history

**Key Benefits:**
- Single source of truth (Git)
- Automated deployments
- Safe and auditable changes

![ArgoCD Applications](image-5.png)

## 🏛️ Architecture Principles

### Clean Architecture
- **Domain Layer**: Business logic and entities
- **Application Layer**: Use cases and CQRS commands/queries
- **Infrastructure Layer**: Database, messaging, external services
- **Presentation Layer**: API controllers and DTOs

### Design Patterns
- **CQRS** - Command Query Responsibility Segregation
- **Mediator Pattern** - Via MediatR
- **Repository Pattern** - Database abstraction
- **Unit of Work** - Transaction management
- **Domain Events** - Domain-driven design
- **Saga Pattern** - Distributed transactions

## 🔧 Features

### Procurement Service
- Create and manage procurement orders
- Idempotency support
- Rate limiting
- Correlation ID tracking
- Domain events

### Supplier Service
- Manage supplier information
- Supplier reservation
- Event-driven communication

### Cross-cutting Concerns
- **Exception Handling** - Global exception middleware
- **Logging** - Structured logging with Serilog
- **Monitoring** - OpenTelemetry and Prometheus
- **Health Checks** - Application health monitoring
- **Rate Limiting** - API rate limiting
- **Correlation ID** - Request tracking

## 🧪 Testing

The project includes the following test types:
- Unit Tests
- Integration Tests
- API Tests
- Performance Tests

## 📚 API Documentation

Swagger UI provides API documentation:
- Development: `http://localhost:5000`
- Swagger JSON: `http://localhost:5000/swagger/v1/swagger.json`

## 🔐 Security

- **Idempotency Keys** - For POST requests
- **Rate Limiting** - API abuse prevention
- **Input Validation** - Data validation
- **Exception Handling** - Secure error handling

## 🚀 Production Deployment

### Kubernetes
```bash
# Install Helm charts
helm install procurement-api ./deploy/helm/procurement-api
helm install supplier-api ./deploy/helm/supplier-api
```

### Docker
```bash
# Build image
docker build -t agriprocurement-api .

# Run container
docker run -p 8080:80 agriprocurement-api
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is distributed under the MIT License. See `LICENSE` file for more information.

## 👨‍💻 Author

**DevOps Engineer**
- Specialist in Kubernetes, GitOps, Monitoring, Cloud-Native Infrastructure
- Created to demonstrate modern DevOps practices and tooling

## 📞 Contact

For questions or suggestions, please create an issue or submit a pull request.

---

**Note:** This project is created for educational and demonstration purposes, showcasing real-world DevOps practices and tooling.
