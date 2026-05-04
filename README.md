📘 EventDrivenOrderProcessing — Event‑Driven Microservices with Kafka, gRPC & .NET
A complete, real‑world demonstration of event‑driven microservices using Kafka, gRPC, and Clean Architecture in .NET.
This solution simulates a production‑grade Order → Inventory → Payment workflow using a combination of:

Asynchronous event choreography (Kafka)

Synchronous RPC communication (gRPC)

Shared event contracts

Clean service boundaries

Structured logging & observability

This project is ideal for developers learning microservices, distributed systems, event-driven architecture, and Kafka integration in .NET.

🚀 Architecture Overview
The system consists of three microservices:

🟦 1. OrderService
Accepts new orders

Calls InventoryService via gRPC to check stock availability

If stock is available → publishes OrderCreated event to Kafka

If not → returns failure response

Uses correlation IDs for traceability

🟩 2. InventoryService (gRPC Server)
Exposes a gRPC endpoint to check stock

Returns availability status

Acts as a synchronous validation layer before events are published

🟧 3. PaymentService
Subscribes to OrderCreated events

Processes payment

Publishes PaymentCompleted event

Logs event lifecycle

🔄 Event Flow (Choreography Pattern)
Code
Client → OrderService → InventoryService (gRPC)
         |
         | Stock Available
         ↓
   Kafka Topic: order-created
         |
         ↓
   PaymentService → Kafka Topic: payment-completed
This architecture demonstrates how event-driven systems often combine async events + sync RPC to balance consistency and responsiveness.

🧩 Key Features
✔ Event‑Driven Communication (Kafka)
Producers & consumers using Confluent.Kafka

Strongly typed event contracts

Topic-based event choreography

Decoupled microservices

✔ gRPC Integration
High‑performance RPC between OrderService and InventoryService

Protobuf contracts

Strong typing and low latency

Ideal for internal microservice communication

✔ Clean Architecture
Domain layer independent of infrastructure

Dependency Inversion applied across services

Testable and maintainable structure

✔ Observability
Structured logging

Correlation IDs

Event lifecycle tracing

Clear logs for each stage of the workflow

✔ Docker Support
Kafka broker

Zookeeper

Microservices

Ready for local orchestration

📁 Solution Structure
Code
EventDrivenOrderProcessing/
│
├── OrderService/
│   ├── Controllers/
│   ├── Services/
│   ├── gRPC Clients/
│   ├── Kafka Producers/
│   └── Application / Domain / Infrastructure
│
├── InventoryService/
│   ├── Protos/
│   ├── gRPC Server/
│   └── Stock Logic
│
├── PaymentService/
│   ├── Kafka Consumers/
│   └── Payment Processing Logic
│
└── Shared/
    ├── Event Contracts
    └── Common Utilities (Correlation IDs, Logging)
🛠 Tech Stack
Backend
.NET 8

C#

Clean Architecture

Messaging
Kafka

Confluent.Kafka client

RPC
gRPC

Protobuf

Containerization
Docker

Docker Compose

▶️ How to Run the Project
1. Start Kafka + Zookeeper
bash
docker-compose up -d
2. Run each microservice
From each service folder:

bash
dotnet run
3. Trigger an order
Send a POST request to OrderService:

json
POST /api/orders
{
  "productId": "P1001",
  "quantity": 2,
  "customerId": "C001"
}
4. Watch the flow
OrderService → gRPC call to InventoryService

OrderService → publishes OrderCreated

PaymentService → consumes event

PaymentService → publishes PaymentCompleted

📚 Learning Outcomes
By exploring this project, you will learn:

How to design event-driven microservices

How to integrate Kafka with .NET

How to use gRPC for synchronous communication

How to design shared event contracts

How to apply Clean Architecture in distributed systems

How to implement correlation IDs for tracing

How to build scalable, decoupled services

🤝 Contributions
Pull requests, issues, and suggestions are welcome.
This project is meant to be a learning resource for the community.

⭐ If you find this useful
Please ⭐ star the repository — it helps others discover it.