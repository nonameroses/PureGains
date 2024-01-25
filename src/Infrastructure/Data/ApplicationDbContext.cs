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
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<MuscleGroup> MuscleGroups => Set<MuscleGroup>();
    public DbSet<WorkoutGroup> WorkoutGroups => Set<WorkoutGroup>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}