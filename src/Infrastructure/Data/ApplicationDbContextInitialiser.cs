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

            await _context.SaveChangesAsync();
        }
        if (!_context.MuscleGroups.Any())
        {
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Back",
                ImagePath = "../assets/muscles/back.png"
            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Biceps",
                ImagePath = "../assets/muscles/biceps.png"

            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Chest",
                ImagePath = "../assets/muscles/chest.png'"

            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Triceps",
                ImagePath = "../assets/muscles/triceps.png"

            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Shoulders",
                ImagePath = "../assets/muscles/shoulder.png"

            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Abs",
                ImagePath = "../assets/muscles/abs.png"

            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Legs",
                ImagePath = "../assets/muscles/legs.png"
            });
            _context.MuscleGroups.Add(new MuscleGroup
            {
                Name = "Full-Body",
                ImagePath = "../assets/muscles/body-builder.png"
            });

            await _context.SaveChangesAsync();

        }
        if (!_context.WorkoutGroups.Any())
        {
            _context.WorkoutGroups.Add(new WorkoutGroup
            {
                Name = "Pull Workout",
            });
            _context.WorkoutGroups.Add(new WorkoutGroup
            {
                Name = "Push Workout",
            });
            _context.WorkoutGroups.Add(new WorkoutGroup
            {
                Name = "Legs Workout",
            });
            _context.WorkoutGroups.Add(new WorkoutGroup
            {
                Name = "Full-Body Workout",
            });
            _context.WorkoutGroups.Add(new WorkoutGroup
            {
                Name = "Abs Workout",
            });
            //_context.WorkoutGroups.Add(new WorkoutGroup
            //{
            //    Name = "Custom Workout",
            //    // MuscleGroups = _context.MuscleGroups.Where(x => x.Id == 1).ToList()
            //});

            await _context.SaveChangesAsync();
        }


        if (!_context.WorkoutGroupTargets.Any())
        {
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Pull Workout
                WorkoutGroupId = 1,
                // Back
                MuscleGroupId = 1,
            });

            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Pull Workout
                WorkoutGroupId = 1,
                // Bicep
                MuscleGroupId = 3,
            });

            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Push Workout
                WorkoutGroupId = 2,
                // Chest
                MuscleGroupId = 2,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Push Workout
                WorkoutGroupId = 2,
                // Triceps
                MuscleGroupId = 4,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Legs Workout
                WorkoutGroupId = 3,
                // Legs 
                MuscleGroupId = 7,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Full-Body Workout
                WorkoutGroupId = 4,
                // Back
                MuscleGroupId = 1,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Full-Body Workout
                WorkoutGroupId = 4,
                // Chest
                MuscleGroupId = 2,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Full-Body Workout
                WorkoutGroupId = 4,
                // Biceps
                MuscleGroupId = 3,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Full-Body Workout
                WorkoutGroupId = 4,
                // Triceps
                MuscleGroupId = 4,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Full-Body Workout
                WorkoutGroupId = 4,
                // Abs
                MuscleGroupId = 5,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Full-Body Workout
                WorkoutGroupId = 4,
                // Shoulders
                MuscleGroupId = 6,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Full-Body Workout
                WorkoutGroupId = 4,
                // Legs
                MuscleGroupId = 7,
            });
            _context.WorkoutGroupTargets.Add(new WorkoutGroupTargets
            {
                // Abs Workout
                WorkoutGroupId = 5,
                // Abs
                MuscleGroupId = 5,
            });


        }


        if (!_context.Muscles.Any())
        {

            _context.Muscles.Add(new Muscle
            {
                Name = "Lats",
                Description = "Latissimus dorsi - the broadest muscle of the back.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Traps",
                Description = "Trapezius - a large muscle extending over the back of the neck and shoulders.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Rhomboids",
                Description = "Rhomboid major and minor - muscles between the shoulder blades.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Deltoids",
                Description = "Shoulder muscles that contribute to shoulder movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 5)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Pectoralis Major",
                Description = "Large chest muscle responsible for chest movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 3)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Pectoralis Minor",
                Description = "Smaller chest muscle located beneath the pectoralis major.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 3)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Biceps Brachii",
                Description = "Muscle in the upper arm responsible for elbow flexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 2)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Triceps Brachii",
                Description = "Muscle in the back of the upper arm responsible for elbow extension.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 4)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Rectus Abdominis",
                Description = "Abdominal muscles commonly known as the 'six-pack'.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 6)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Obliques",
                Description = "Muscles on the sides of the abdominal area responsible for rotation.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 6)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Quadriceps",
                Description = "Front thigh muscles responsible for knee extension.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Hamstrings",
                Description = "Muscles at the back of the thigh responsible for knee flexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Gastrocnemius",
                Description = "Calf muscle responsible for ankle movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Soleus",
                Description = "Calf muscle beneath the gastrocnemius also involved in ankle movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Tibialis Anterior",
                Description = "Muscle on the front of the lower leg responsible for dorsiflexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Forearms",
                Description = "Muscles of the forearm responsible for wrist and finger movements.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 2)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Glutes",
                Description = "Muscles of the buttocks responsible for hip movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Calves",
                Description = "Muscles of the lower leg responsible for ankle movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Latissimus Dorsi",
                Description = "Large, flat muscles on the back responsible for shoulder and arm movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Teres Major",
                Description = "Muscle of the upper back involved in shoulder movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Teres Minor",
                Description = "Muscle of the upper back involved in shoulder movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Infraspinatus",
                Description = "Muscle of the upper back involved in shoulder movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Supraspinatus",
                Description = "Muscle of the upper back involved in shoulder movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Subscapularis",
                Description = "Muscle of the upper back involved in shoulder movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Serratus Anterior",
                Description = "Muscle on the side of the chest involved in shoulder movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Erector Spinae",
                Description = "Group of muscles that straighten and rotate the back.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "External Obliques",
                Description = "Muscles on the sides of the abdominal area responsible for rotation.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Internal Obliques",
                Description = "Muscles on the sides of the abdominal area responsible for rotation.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Transverse Abdominis",
                Description = "Muscle that wraps around the abdomen, providing stability.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Serratus Posterior",
                Description = "Muscles of the upper back and neck involved in breathing.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Levator Scapulae",
                Description = "Muscle that elevates the scapula.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Scalenes",
                Description = "Muscles of the neck involved in breathing and neck movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 5)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Rotator Cuff",
                Description = "Group of muscles that stabilize the shoulder joint.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 5)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Anterior Deltoid",
                Description = "Front part of the shoulder muscle responsible for arm flexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 5)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Medial Deltoid",
                Description = "Middle part of the shoulder muscle responsible for arm abduction.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 5)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Posterior Deltoid",
                Description = "Rear part of the shoulder muscle responsible for arm extension.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 5)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Sternocleidomastoid",
                Description = "Muscle of the neck responsible for head rotation and flexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 5)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Subclavius",
                Description = "Muscle beneath the clavicle responsible for shoulder stabilization.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 3)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Intercostals",
                Description = "Muscles between the ribs involved in breathing.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 3)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Brachialis",
                Description = "Muscle in the upper arm that flexes the elbow.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 2)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Brachioradialis",
                Description = "Muscle in the forearm that flexes the elbow.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 2)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Anconeus",
                Description = "Small muscle in the back of the arm that assists in elbow extension.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 4)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Pronator Teres",
                Description = "Muscle in the forearm that pronates the hand and forearm.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 2)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Supinator",
                Description = "Muscle in the forearm that supinates the hand and forearm.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 2)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Quadratus Lumborum",
                Description = "Muscle of the lower back involved in spinal movement and stability.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Iliopsoas",
                Description = "Group of muscles that flex the hip joint.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Tensor Fasciae Latae",
                Description = "Muscle of the hip responsible for hip flexion and abduction.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Adductors",
                Description = "Muscles of the inner thigh responsible for hip adduction.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Gracilis",
                Description = "Muscle of the inner thigh involved in hip adduction and knee flexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Pectineus",
                Description = "Muscle of the inner thigh responsible for hip flexion and adduction.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Peroneus Longus",
                Description = "Muscle of the lower leg responsible for ankle eversion and plantarflexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Peroneus Brevis",
                Description = "Muscle of the lower leg responsible for ankle eversion and plantarflexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Anterior Tibialis",
                Description = "Muscle on the front of the lower leg responsible for dorsiflexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
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




    }
}


