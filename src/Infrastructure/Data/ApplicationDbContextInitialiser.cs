using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    //private readonly UserManager<ApplicationUser> _userManager;
    // private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
        //_userManager = userManager;
        //  _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Equipment.Any())
        {
            _context.Equipment.Add(new Equipment { Name = "Kettlebell", ImagePath = "../assets/equipment/kettlebell-black.png" });
            _context.Equipment.Add(new Equipment { Name = "Dumbbell", ImagePath = "../assets/equipment/dumbbell-black.png" });
            _context.Equipment.Add(new Equipment { Name = "Barbell", ImagePath = "../assets/equipment/barbell-black.png" });
            _context.Equipment.Add(new Equipment { Name = "Plate", ImagePath = "../assets/equipment/weight-plates-black.png" });
            _context.Equipment.Add(new Equipment { Name = "Pull-up Bar", ImagePath = "../assets/equipment/pull-up-bar.png" });
            _context.Equipment.Add(new Equipment { Name = "Bench", ImagePath = "../assets/equipment/bench-press.png" });
            _context.Equipment.Add(new Equipment { Name = "Band", ImagePath = "../assets/equipment/resistance-band.png" });
            _context.Equipment.Add(new Equipment { Name = "Bodyweight", ImagePath = "../assets/equipment/bodyweight.png" });

            // Default roles
            //var administratorRole = new IdentityRole(Roles.Administrator);

            //if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            //{
            //    await _roleManager.CreateAsync(administratorRole);
            //}

            //// Default users
            //var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            //if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            //{
            //    await _userManager.CreateAsync(administrator, "Administrator1!");
            //    if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            //    {
            //        await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            //    }
            //}


            await _context.SaveChangesAsync();
        }


        if (_context.MuscleGroups.Any())
        {
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Back"
            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Chest"
            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Biceps"
            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Triceps"
            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Abs"
            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Shoulders"
            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Legs"
            });

            await _context.SaveChangesAsync();

        }


        if (!_context.Muscles.Any())
        {
            _context.Muscles.Add(new Muscle
            {
                Name = "Lats",
                Description = "Latissimus dorsi - the broadest muscle of the back.",
                MuscleGroup = new MuscleGroup { Id = 1 }
            });
            _context.Muscles.Add(new Muscle
            {
                Name = "Rhomboids",
                Description = "Rhomboid major and minor - muscles between the shoulder blades.",
                MuscleGroup = new MuscleGroup { Id = 1 }
            });




            await _context.SaveChangesAsync();
        }


        //if (!_context.Exercises.Any())
        //{
        //    _context.Exercises.Add(new Exercise
        //    {
        //        Id = 1,
        //        Name = "Pull-Up",
        //        Equipment = new Equipment { Id = 5 },
        //        PrimaryMuscle = new Muscle { Id = 1 },
        //        SecondaryMuscle = new Muscle { Id = 2 },
        //        Priority = 1,
        //        Description = "",
        //    });

        //    await _context.SaveChangesAsync();
        //}






        //if (!_context.WorkoutGroups.Any())
        //{
        //    _context.WorkoutGroups.Add(new WorkoutGroup
        //    {
        //        Id = 1,
        //        Name = "Pull",
        //        MuscleGroup = new MuscleGroup
        //        {
        //            Id = 1
        //        }
        //    });

        //    await _context.SaveChangesAsync();
        //}


        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Traps",
        //    Description = "Trapezius - a large muscle extending over the back of the neck and shoulders.",
        //    MuscleGroupId = 1
        //});

        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Deltoids",
        //    Description = "Shoulder muscles that contribute to shoulder movement.",
        //    MuscleGroupId = 6
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Pectoralis Major",
        //    Description = "Large chest muscle responsible for chest movement.",
        //    MuscleGroupId = 2
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Pectoralis Minor",
        //    Description = "Smaller chest muscle located beneath the pectoralis major.",
        //    MuscleGroupId = 2
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Biceps Brachii",
        //    Description = "Muscle in the upper arm responsible for elbow flexion.",
        //    MuscleGroupId = 3
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Triceps Brachii",
        //    Description = "Muscle in the back of the upper arm responsible for elbow extension.",
        //    MuscleGroupId = 4
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Rectus Abdominis",
        //    Description = "Abdominal muscles commonly known as the 'six-pack'.",
        //    MuscleGroupId = 5
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Obliques",
        //    Description = "Muscles on the sides of the abdominal area responsible for rotation.",
        //    MuscleGroupId = 5
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Quadriceps",
        //    Description = "Front thigh muscles responsible for knee extension.",
        //    MuscleGroupId = 7
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Hamstrings",
        //    Description = "Muscles at the back of the thigh responsible for knee flexion.",
        //    MuscleGroupId = 7
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Gastrocnemius",
        //    Description = "Calf muscle responsible for ankle movement.",
        //    MuscleGroupId = 7
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Soleus",
        //    Description = "Calf muscle beneath the gastrocnemius also involved in ankle movement.",
        //    MuscleGroupId = 7
        //});
        //_context.Muscles.Add(new Muscle
        //{
        //    Name = "Tibialis Anterior",
        //    Description = "Muscle on the front of the lower leg responsible for dorsiflexion.",
        //    MuscleGroupId = 7
        //});


    }
}


