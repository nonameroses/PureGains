using MediatR;

namespace Application.Features.Workout.Commands;

public static class AddWorkout
{
    public sealed record Command : IRequest<Domain.Entities.Workout>
    {
        public readonly Domain.Entities.Workout Workout;
        public Command(Domain.Entities.Workout workout)
        {
            Workout = workout;
        }
    }

    public sealed class Handler : IRequestHandler<Command, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventBus;

        public Handler(IProductRepository repository, IMapper mapper, IEventPublisher eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<ProductDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var productToAdd = Product.Create(
                request.AddProductDto.Name,
                request.AddProductDto.Details,
                request.AddProductDto.Code,
                request.AddProductDto.Cost,
                request.AddProductDto.Price,
                request.AddProductDto.AlertQuantity,
                request.AddProductDto.TrackQuantity,
                request.AddProductDto.Quantity);

            await _repository.AddAsync(productToAdd, cancellationToken);
            foreach (var @event in productToAdd.DomainEvents)
            {
                await _eventBus.PublishAsync(@event, token: cancellationToken);
            }
            return _mapper.Map<ProductDto>(productToAdd);
        }
    }
}
