using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Events;

public record OrderCreated(Guid OrderId, decimal Amount, string CustomerEmail);
public record PaymentProcessed(Guid OrderId, Guid PaymentId, decimal Amount);
public record PaymentFailed(Guid OrderId, string Reason);
public record ShipmentCreated(Guid OrderId, Guid ShipmentId, string TrackingNumber);
public record ShipmentFailed(Guid OrderId, string Reason);
public record OrderCompleted(Guid OrderId);
public record OrderCancelled(Guid OrderId, string Reason);
