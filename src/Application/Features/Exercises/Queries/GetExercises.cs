using Application.Common.Interfaces;
using Application.Features.Exercises.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Exercises.Queries;

//Query to get all the Equipment 
public class GetExercises
{
    public sealed class Query : IRequest<IEnumerable<Exercise>>
    {
        public ExerciseRequestDto Exercises { get; set; }

        public Query(ExerciseRequestDto exercises)
        {
            Exercises = exercises;
        }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Exercise>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exercise>> Handle(Query request, CancellationToken cancellationToken)
        {
            var exercises = await _context.Exercises.Where(e =>
                    request.Exercises.EquipmentIds.Contains(e.EquipmentId) &&
                    request.Exercises.MuscleGroupIds.Contains(e.PrimaryMuscleGroupId))
                    .ToListAsync(cancellationToken);

            //var exercises = await _context.Exercises.Where(e =>
            //        e.EquipmentId == exercises.EquipmentId && e.PrimaryMuscleGroupId == exercises.MuscleGroupId)
            //    .ToListAsync(cancellationToken);

            return exercises;
        }
    }
}