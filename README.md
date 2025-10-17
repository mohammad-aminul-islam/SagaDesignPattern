### 🧩 Saga Choreography Event Flow (E-Commerce Example with Failures)

```mermaid
sequenceDiagram
    participant OrderService
    participant PaymentService
    participant InventoryService
    participant DeliveryService

    OrderService->>PaymentService: Publishes **OrderCreated**
    PaymentService->>InventoryService: Processes payment → Publishes **PaymentCompleted**
    PaymentService-->>OrderService: On failure → Publishes **PaymentFailed**

    InventoryService->>DeliveryService: Reserves items → Publishes **StockReserved**
    InventoryService-->>PaymentService: On failure → Publishes **StockFailed**

    DeliveryService-->>OrderService: Arranges delivery → Publishes **DeliveryAssigned**
    DeliveryService-->>OrderService: On failure → Publishes **DeliveryFailed**
