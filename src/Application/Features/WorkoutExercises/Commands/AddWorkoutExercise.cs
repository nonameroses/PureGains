using Domain.Entities;
using MediatR;

namespace Application.Features.WorkoutExercises.Commands;

public static class CreateWorkoutExercise
{
    public sealed record Command : IRequest<Workout>
    {
        public readonly AddProductDto AddProductDto;
        public Command(AddProductDto addProductDto)
        {
            AddProductDto = addProductDto;
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
