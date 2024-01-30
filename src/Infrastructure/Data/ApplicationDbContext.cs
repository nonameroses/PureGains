using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<Muscle> Muscles => Set<Muscle>();

    public DbSet<MuscleGroup> MuscleGroups => Set<MuscleGroup>();
    public DbSet<WorkoutGroup> WorkoutGroups => Set<WorkoutGroup>();
    public DbSet<WorkoutGroupTargets> WorkoutGroupTargets => Set<WorkoutGroupTargets>();

    public DbSet<Exercise> Exercises => Set<Exercise>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}

