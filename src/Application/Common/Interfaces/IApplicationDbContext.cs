using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Equipment> Equipment { get; }
    DbSet<WorkoutGroup> WorkoutGroups { get; }
    DbSet<MuscleGroup> MuscleGroups { get; }
    // DbSet<Muscle> Muscles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}