using AgriProcurement.Procurement.Application.Interfaces;
using AgriProcurement.Procurement.Domain.Aggregates;
using AgriProcurement.Procurement.Domain.Entities;
using AgriProcurement.Procurement.Domain.Repositories;
using AgriProcurement.Procurement.Domain.ValueObjects;

namespace AgriProcurement.Procurement.Application.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly IIdempotencyService _idempotencyService;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher,
        IIdempotencyService idempotencyService)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _idempotencyService = idempotencyService;
    }

    public async Task<CreateOrderResult> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        // 1️⃣ Idempotency check
        if (await _idempotencyService.IsProcessedAsync(
                command.IdempotencyKey, cancellationToken))
        {
            return new CreateOrderResult(command.OrderId, "ALREADY_PROCESSED");
        }

        // 2️⃣ Create Aggregate
        var order = new ProcurementOrder(command.OrderId);

        foreach (var line in command.Lines)
        {
            var money = new Money(line.Price, line.Currency);
            var orderLine = new OrderLine(
                line.ProductId,
                line.Quantity,
                money);

            order.AddLine(orderLine);
        }

        // 3️⃣ Domain logic
        var domainEvent = order.Create();

        // 4️⃣ Persist aggregate
        // await _orderRepository.AddAsync(order, cancellationToken);

        // // 5️⃣ Commit transaction
        // await _unitOfWork.CommitAsync(cancellationToken);

        // 6️⃣ Publish domain event (OUTBOX orqali ketadi)
        await _eventPublisher.PublishAsync(domainEvent, cancellationToken);

        // 7️⃣ Mark idempotency
        await _idempotencyService.MarkAsProcessedAsync(
            command.IdempotencyKey, cancellationToken);

        // return new CreateOrderResult(order.Id, order.Status.ToString());
        return new CreateOrderResult(
            OrderId: Guid.NewGuid()
);

    }
}
