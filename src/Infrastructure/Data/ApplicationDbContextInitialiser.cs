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

        //if (!_context.Users.Any())
        //{
        //    _context.Users.Add(new User
        //    {
        //        Username = "test"
        //    });

        //    await _context.SaveChangesAsync();
        //}

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
                Name = "Serratus Anterior",
                Description = "The main actions are protraction and upward rotation of the scapulothoracic joint. It's also a key scapular stabilizer, keeping the shoulder blades against the ribcage when at rest and during movement.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 3)
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
                Name = "Biceps",
                Description = "Muscle in the upper arm responsible for elbow flexion.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 2)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Triceps",
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
                Name = "Lower Abs",
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
                Name = "Erector Spinae",
                Description = "Group of muscles that straighten and rotate the back.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 1)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Internal Obliques",
                Description = "Muscles on the sides of the abdominal area responsible for rotation.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 6)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Transverse Abdominis",
                Description = "Muscle that wraps around the abdomen, providing stability.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 6)
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
                Name = "Rear Delts",
                Description = "The rear delts help to stabilise the shoulders, and work alongside the muscles in your back to prevent your shoulders from hunching forward.",
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
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 7)
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
                Name = "Hip Flexors",
                Description = "The hip flexors are a group of muscles responsible for flexing the hip, or bringing the leg upward toward the body.",
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


        if (!_context.Exercises.Any())
        {
            //Back Exercises

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Swings",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Swing the kettlebell between your legs and up to chest level",
                YoutubeUrl = "https://www.youtube.com/watch?v=YSxHifyI6s8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Row",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Bend forward and pull the kettlebell towards your waist",
                YoutubeUrl = "https://www.youtube.com/watch?v=u4sG54-HKJw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Deadlift",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lift the kettlebell from the ground to a standing position",
                YoutubeUrl = "https://www.youtube.com/watch?v=-8JbTKR50rk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single-Arm Kettlebell Row",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Bend forward and pull the kettlebell with one arm",
                YoutubeUrl = "https://www.youtube.com/watch?v=pYcpY20QaE8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell High Pull",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Pull the kettlebell up to your shoulder, leading with your elbow",
                YoutubeUrl = "https://www.youtube.com/watch?v=0B5JwVgFLP4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Renegade Row",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "In a push-up position with hands on two kettlebells, row one kettlebell up while stabilizing your body with the other arm.",
                YoutubeUrl = "https://www.youtube.com/watch?v=Zp26q4BY5HE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Bend over a bench and row the dumbbell back towards your hip.",
                YoutubeUrl = "https://www.youtube.com/watch?v=pYcpY20QaE8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Deadlift",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Stand with feet hip-width apart, squat down to pick up the dumbbells, and stand up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=ytGaGIn3SjE"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Pullover",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Lie on a bench holding a dumbbell with both hands above your chest, then lower it behind your head and bring it back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=0G2_XV7slIg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single Arm Dumbbell Deadlift",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Perform a deadlift while holding a dumbbell in one hand, switching hands after a set.",
                YoutubeUrl = "https://www.youtube.com/watch?v=sq4VAZ1TtRw"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Bent Over Two-Dumbbell Row",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "With a dumbbell in each hand, bend over at about a 45-degree angle and row the weights back towards your hips.",
                YoutubeUrl = "https://www.youtube.com/watch?v=L2fvpxrfJfQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Bend over and pull the barbell towards your waist, keeping your back straight.",
                YoutubeUrl = "https://www.youtube.com/watch?v=G8l_8chR5BE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Deadlift",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Stand with your mid-foot under the barbell, bend over and grab it, then stand up with the weight.",
                YoutubeUrl = "https://www.youtube.com/watch?v=3UwO0fKukRw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "T-Bar Row",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Load one end of a barbell, straddle it, and row the bar towards your chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=j3Igk5nyZE4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Good Morning",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Hold a barbell on your shoulders behind your neck, then bend at the waist and return.",
                YoutubeUrl = "https://www.youtube.com/watch?v=PLHY2-nt-y4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Rack Pulls",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Set up a barbell at knee height in a rack, then perform deadlifts from this elevated position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=K3izzg0RCTg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Pull-Ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Grip the bar with palms facing away from you and pull your body up until your chin is over the bar.",
                YoutubeUrl = "https://www.youtube.com/watch?v=eGo4IYlbE5g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Chin-Ups",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Grip the bar with palms facing towards you and pull your body up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=brhRXlOhsAM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Negative Pull-Ups",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Start at the top of the pull-up position and slowly lower yourself down with control.",
                YoutubeUrl = "https://www.youtube.com/watch?v=OYUxXMGVuuU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Wide Grip Pull-Ups",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Perform pull-ups with a wider than shoulder-width grip to target the lats more effectively.",
                YoutubeUrl = "https://www.youtube.com/watch?v=eGo4IYlbE5g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hanging Leg Raise",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar and raise your legs to 90 degrees or higher.",
                YoutubeUrl = "https://www.youtube.com/watch?v=hdng3Nm1x_E"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Pull Aparts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Hold a resistance band in front of you and pull it apart until it touches your chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=JObYtU7Y7ag"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Seated Row with Band",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Sit on the floor, legs extended, with a band wrapped around your feet. Row the band towards your waist.",
                YoutubeUrl = "https://www.youtube.com/watch?v=GZbfZ033f74"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Banded Deadlifts",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Stand on a band with feet shoulder-width apart, grab the ends, and stand up straight.",
                YoutubeUrl = "https://www.youtube.com/watch?v=lFqAijL2rJ4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bent-Over Band Rows",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Stand on a band, hinge at the waist, and row the ends towards your waist.",
                YoutubeUrl = "https://www.youtube.com/watch?v=rloXYB8M3vU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Banded Good Mornings",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Place a band under your feet and over your shoulders. Bend at the hips to lower your torso, then stand back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=CTRaCzE8pWU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Inverted Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Using a low bar or rings, pull yourself up from a horizontal position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=dvkIaarnf0g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Back Extensions",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Lie face down on the ground or a hyperextension bench and lift your upper body.",
                YoutubeUrl = "https://www.youtube.com/watch?v=ph3pddpKzzw"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Reverse Snow Angels",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie face down and move your arms from your sides to overhead in a snow angel motion while keeping your arms straight and off the ground.",
                YoutubeUrl = "https://www.youtube.com/watch?v=2VmprBYtCug",
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Superman",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Lie face down on the ground, extend your arms in front of you, and simultaneously lift your arms, chest, and legs off the ground.",
                YoutubeUrl = "https://www.youtube.com/watch?v=z6PJMT2y8GQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plank with Arm Lift",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Get into a plank position and lift one arm straight out in front of you, alternate arms while maintaining plank position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=RDxjPjIrmcE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Hyperextensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Lie face down on a bench with your hips at the edge so your upper body can move freely. Lower and raise your torso.",
                YoutubeUrl = "https://www.youtube.com/watch?v=qtjJUWCnDyE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Bench Row",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Lay chest down on an incline bench and row dumbbells towards your waist.",
                YoutubeUrl = "https://www.youtube.com/watch?v=HsD1vMzp9lI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Decline Bench Dumbbell Pull-Over",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Lying on a decline bench, hold a dumbbell with both hands above your chest and lower it behind your head.",
                YoutubeUrl = "https://www.youtube.com/watch?v=FVjtOSA-dz8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Prone Cobra",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie face down on a bench with arms hanging down. Lift your chest and arms up, squeezing your shoulder blades together.",
                YoutubeUrl = "https://www.youtube.com/watch?v=example"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Reverse Hyperextension on Bench",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Lie face down on a bench with your legs hanging off the edge. Lift your legs up and back, focusing on contracting your lower back and glutes.",
                YoutubeUrl = "https://www.youtube.com/watch?v=example"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Bent Over Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hold a weight plate with both hands, bend at your waist and row the plate towards your stomach.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Deadlift",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Stand with feet hip-width apart, squat down and lift a weight plate from the ground to a standing position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Standing Plate Shrug",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Hold a weight plate in each hand at your sides and shrug your shoulders to work the traps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Good Mornings",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Hold a weight plate against your chest, bend forward at your waist with a slight bend in your knees, then return to standing.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Reverse Snow Angels with Plate",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie face down on the floor holding a lightweight plate in each hand, perform a reverse snow angel motion by moving your arms from your sides to overhead.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Bench Press",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench and press the kettlebells up, keeping your palms facing each other.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Flyes",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on a bench, hold kettlebells in each hand above your chest with a slight bend in your elbows, then lower them out to the sides and bring them back.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Pullover",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lie on your back, hold a kettlebell with both hands above your chest, then lower it behind your head and bring it back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single Arm Kettlebell Bench Press",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench and press one kettlebell up, alternating arms.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Pushup",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Place two kettlebells on the ground and perform pushups, using the kettlebells as handles.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench and press the kettlebells up, keeping your palms facing each other.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Flyes",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on a bench, hold kettlebells in each hand above your chest with a slight bend in your elbows, then lower them out to the sides and bring them back.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Pullover",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lie on your back, hold a kettlebell with both hands above your chest, then lower it behind your head and bring it back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single Arm Kettlebell Bench Press",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench and press one kettlebell up, alternating arms.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Pushup",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Place two kettlebells on the ground and perform pushups, using the kettlebells as handles.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench and press the dumbbells up from your chest until your arms are straight.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Flyes",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on a bench, extend your arms above your chest with a dumbbell in each hand, then lower your arms out to the sides and bring them back together.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Dumbbell Press",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Sit on an incline bench and press the dumbbells from shoulder height until your arms are straight above you.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Decline Press",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a decline bench and press the dumbbells up from your chest until your arms are straight.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Around The World",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a flat bench with a dumbbell in each hand at your sides. Circle the weights up above your chest and then back down in a wide arc.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench and press a barbell up from your chest until your arms are straight.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Barbell Press",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on an incline bench and press a barbell from chest height until your arms are straight.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Decline Barbell Press",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a decline bench and press a barbell up from your chest until your arms are straight.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Pullover",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lie on your back on a bench, hold a barbell with both hands above your chest, then lower it behind your head and bring it back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close Grip Barbell Bench Press",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench and press a barbell up from your chest with your hands closer together than in a regular bench press.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Chest Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Perform a pull-up but focus on bringing your chest to the bar by leaning back slightly.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Around The World Pull-ups",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Start with a regular pull-up, then move in a circular motion bringing your chest towards one hand, then up and over to the other side.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Clapping Pull-up",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Perform an explosive pull-up and clap your hands together above the bar before catching yourself on the way down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Muscle-up",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Perform a pull-up followed by a dip, transitioning from below to above the bar in one fluid motion.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Straight Bar Dips",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Using a straight or parallel bar, perform dips with a slight forward lean to emphasize the chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Dips",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Facing away from the bench, place your hands on it and lower your body by bending your elbows, then push back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Push-up",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Place your hands on a bench for an elevated push-up, focusing on the lower chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Decline Push-up",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "With your feet elevated on a bench and hands on the ground, perform a push-up to target the upper chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Press to Neck",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench with a barbell, lowering it slowly towards your neck rather than the chest, then press it up. Be cautious with the weight to avoid injury.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Explosive Push-ups",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Using a bench for either hand placement or feet placement, perform a push-up with enough force to lift your body off the ground.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Anchor the band behind you and press forward as if performing a bench press.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Fly",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Anchor the band at chest level and perform a fly movement, bringing your hands together in front of you.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Standing Band Press",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Stand on the band and press upwards, simulating a shoulder press but focusing on using the chest muscles.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Pullovers",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lie on your back and use a band to perform the pullover movement, stretching and contracting the chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single-arm Band Fly",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Anchor the band at chest level and perform a single-arm fly, isolating one side of the chest at a time.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on your back and press a weight plate up from your chest until your arms are straight, similar to a bench press.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Squeeze Press",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Hold two plates together at chest level and press them together as hard as you can while pressing them upwards.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Lying Plate Pullover",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lie on the ground or a bench, hold a plate with both hands and extend your arms behind your head then bring it back over your chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Standing Plate Chest Press",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Stand and hold a weight plate at chest level. Press the plate straight out in front of you, then bring it back.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Around The World",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Hold a plate in front of your thighs and circle it up and around your head in a continuous motion to target the chest from all angles.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Push-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Standard push-ups engage the chest, shoulders, and triceps, with hands shoulder-width apart.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Wide Grip Push-up",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Perform push-ups with your hands set wider than shoulder-width to target the outer chest more effectively.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Diamond Push-up",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Perform push-ups with your hands close together under your chest, forming a diamond shape with your thumbs and index fingers, to focus on the inner chest and triceps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Decline Push-up",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Elevate your feet on a bench or step and perform push-ups to increase the intensity on the upper chest and shoulders.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Push-up",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Place your hands on an elevated surface such as a bench or step and perform push-ups to target the lower chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Decline Push-up",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Elevate your feet on a bench or step and perform push-ups to increase the intensity on the upper chest and shoulders.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Push-up",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Place your hands on an elevated surface such as a bench or step and perform push-ups to target the lower chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Floor Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on the floor and press kettlebells upwards, similar to a bench press but with a limited range of motion.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Seated Kettlebell Press",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Sit upright on the floor or a bench and press kettlebells above your head, focusing on engaging the chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Chest Fly",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on a bench and perform a fly movement with kettlebells, extending your arms wide and then bringing them together above your chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Pullover on Floor",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lie on the floor holding a kettlebell with both hands above your chest, extend your arms behind your head, then bring the kettlebell back to the starting position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Squeeze Press",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench and press two kettlebells up while squeezing them together as hard as you can throughout the entire movement.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Floor Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on the floor and press kettlebells upwards, similar to a bench press but with a limited range of motion.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Seated Kettlebell Press",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Sit upright on the floor or a bench and press kettlebells above your head, focusing on engaging the chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Chest Fly",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on a bench and perform a fly movement with kettlebells, extending your arms wide and then bringing them together above your chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Pullover on Floor",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lie on the floor holding a kettlebell with both hands above your chest, extend your arms behind your head, then bring the kettlebell back to the starting position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Squeeze Press",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a bench and press two kettlebells up while squeezing them together as hard as you can throughout the entire movement.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Isometric Chest Holds",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Grip the pull-up bar with an overhand grip and pull yourself halfway up. Hold this position to engage your chest muscles.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Pull-over with Leg Raise",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Hang from the bar, perform a pull-over by lifting your body up and over the bar while simultaneously raising your legs straight in front of you.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Clap Pull-ups",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Perform an explosive pull-up and at the top of the movement, release the bar, clap your hands, and then grab the bar again.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Archer Pull-ups",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Perform a pull-up but with one arm extended to the side like an archer, alternating the working arm with each rep.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Typewriter Pull-ups",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Pull yourself up and move laterally across the bar, mimicking the action of a typewriter carriage.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Bicep Curl",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the kettlebell with both hands and perform a curl by bringing it towards your chest, then lowering it back down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single-Arm Kettlebell Curl",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in one hand with a neutral grip and curl it towards your shoulder. Repeat on both sides.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Hammer Curl",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Perform a curl with the kettlebell by holding the handle and keeping your thumb facing upwards, mimicking a hammer grip.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Concentration Curl",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit down with your elbow on your thigh and curl the kettlebell towards your shoulder, focusing on isolating the bicep.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Alternating Kettlebell Curl",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand with a kettlebell in each hand and alternate curling each one towards your shoulder.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Bicep Curl",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the kettlebell with both hands and perform a curl by bringing it towards your chest, then lowering it back down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single-Arm Kettlebell Curl",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in one hand with a neutral grip and curl it towards your shoulder. Repeat on both sides.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Hammer Curl",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Perform a curl with the kettlebell by holding the handle and keeping your thumb facing upwards, mimicking a hammer grip.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Concentration Curl",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit down with your elbow on your thigh and curl the kettlebell towards your shoulder, focusing on isolating the bicep.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Alternating Kettlebell Curl",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand with a kettlebell in each hand and alternate curling each one towards your shoulder.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Bicep Curl",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand or sit with a dumbbell in each hand, palms facing forward, and curl the weights while keeping your elbows close to your torso.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Dumbbell Curl",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie back on an incline bench with a dumbbell in each hand and curl the weights towards your shoulders.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hammer Curl",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold dumbbells at your side with palms facing your torso and curl the weights up while keeping your thumbs pointing up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Preacher Curl",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit at a preacher bench with a dumbbell in hand, palm facing up. Curl the dumbbell towards your shoulder, focusing on isolating the bicep.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Concentration Curl",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench with a dumbbell in one hand, and brace your elbow against your thigh. Curl the dumbbell towards your shoulder, isolating the bicep.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Curl",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand with feet shoulder-width apart, grip the barbell with hands shoulder-width apart, and curl the bar towards your chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "EZ Bar Preacher Curl",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Use an EZ curl bar and perform curls on a preacher bench to isolate the biceps effectively.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Standing Barbell Reverse Curl",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a barbell with an overhand grip and curl it towards your chest, engaging both the biceps and the forearms.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close-Grip Barbell Curl",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Perform a barbell curl with your hands placed closer than shoulder-width apart to target the inner biceps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Drag Curl",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Curl the barbell by dragging it up your body as close as possible, which helps to engage the biceps more intensely.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Chin-Ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Grip the pull-up bar with palms facing towards you and pull yourself up until your chin is over the bar.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Negative Chin-Ups",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Start at the top of the chin-up position and slowly lower yourself down with control to emphasize the eccentric phase.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Mixed Grip Pull-up",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Grip the bar with one hand facing towards you and the other facing away. Alternate grips between sets.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Commando Pull-ups",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Grip the bar with both hands next to each other, one facing forward and one back. Pull yourself up, alternating sides.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close-Grip Chin-Ups",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Perform chin-ups with your hands placed close together to increase the focus on the biceps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Standing Band Bicep Curl",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band and curl your hands towards your shoulders.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Seated Band Bicep Curl",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a chair and anchor the band under your feet. Curl your hands towards your shoulders.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Hammer Curl",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand on the band and perform curls with palms facing each other, mimicking the hammer curl motion.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Preacher Curl",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Secure the band under your feet and lean forward slightly. Curl the band towards you, mimicking the motion of a preacher curl.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Concentration Curl",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit down, anchor the band under your foot, and perform curls focusing on isolating the biceps, similar to a dumbbell concentration curl.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Curl",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a weight plate at its edge with both hands and perform curls, bringing it towards your chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Standing Plate Hammer Curl",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Grip the plate from its sides and perform a curl with your thumbs pointing upwards, mimicking a hammer curl.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single-Arm Plate Curl",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a plate in one hand at the side and perform curls, isolating one arm at a time.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Preacher Curl",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Using a preacher bench, rest your arm and perform curls with a plate, focusing on isolating the bicep.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Zottman Curl",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Perform a curl with a plate, but at the top of the movement, rotate your grip so your palm faces down before lowering, then rotate back at the bottom.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bodyweight Bicep Curl",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Use a sturdy table or a bar at hip height. Grab the edge/bar with palms facing up, lean back, and curl your body up towards your hands.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Chin-Ups",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from a pull-up bar with palms facing towards you and pull yourself up until your chin is over the bar.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Commando Chin-ups",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from a pull-up bar with one hand in front of the other, facing you. Pull up, alternating the side of your head that clears the bar.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Isometric Chin-up Hold",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Pull yourself up in a chin-up position and hold at the top for as long as possible to engage the biceps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Towel Bicep Curl",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Wrap a towel around a sturdy bar, hold the ends, lean back, and perform curls by pulling yourself towards the bar.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Overhead Triceps Extension",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the kettlebell by the handle with both hands behind your head, extend your arms to lift the kettlebell overhead.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Skull Crusher",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench, hold a kettlebell with both hands, and perform a triceps extension by lowering it towards your forehead and then extending your arms.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single-Arm Kettlebell Floor Press",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on the floor, hold a kettlebell in one hand with your arm fully extended. Lower the kettlebell by bending your elbow, then press it back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Tricep Kickback",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Bend forward slightly, hold a kettlebell in one hand, keep your elbow close to your body, and extend your arm back.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Close-Grip Floor Press",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on the floor and press two kettlebells up with your hands close together, focusing on the triceps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Overhead Triceps Extension",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell with both hands and extend your arms overhead. Bend your elbows to lower the dumbbell behind your head, then extend your arms.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Kickback",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "With one knee and hand on a bench, hold a dumbbell in the other hand, keep your upper arm stationary, and extend your arm to kick back.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Skull Crusher",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench with dumbbells in each hand. Extend your arms above " +
                              "you and bend your elbows to lower the dumbbells towards your forehead.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Tricep Press",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench and hold a dumbbell with both hands above your chest. Keep your elbows in and lower the dumbbell towards your chest by bending your elbows, then press it back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Dumbbell Skull Crusher",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on an incline bench with a dumbbell in each hand. Extend your arms and then bend your elbows to lower the dumbbells towards your temples before extending back.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Skull Crusher",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench and hold a barbell above your chest. Bend at your elbows to lower the bar towards your forehead, then extend your arms to return to the starting position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close-Grip Barbell Bench Press",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench and grip the barbell with hands closer than shoulder-width. Lower the bar to your chest, then press it back up, focusing on using your triceps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Overhead Triceps Extension",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand or sit with a barbell held overhead. Bend your elbows to lower the bar behind your head, then extend your arms to lift the bar back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Tricep Press",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench and hold a barbell with a narrow grip. Start with the bar directly above you, lower it towards your chest by bending your elbows, then press it back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Reverse Grip Barbell Bench Press",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench and grip the barbell with an underhand grip. Press the bar up and down while keeping your wrists straight and focusing on engaging your triceps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bodyweight Triceps Extension",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Use a low bar or rings. Keep your body straight and lean forward, placing your hands on the bar. Bend at your elbows to lower your forehead towards the bar, then push back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Triceps Dips on Parallel Bars",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Grip the parallel bars, lift yourself up, then lower your body by bending your elbows until they're at about 90 degrees, push back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Pull-up Bar Triceps Press",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand beneath a pull-up bar, reach up and place your hands on the bar with a narrow grip, and press down as if performing a triceps dip.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Reverse Grip Pull-up",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Perform a pull-up with an underhand grip, focusing on using your triceps to lift your body.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Isometric Hold Triceps Extension",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Use a low bar or rings to hold yourself in the bottom position of a triceps extension, keeping your body straight and elbows bent, hold as long as possible.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Triceps Pushdown",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a band above you and perform triceps pushdowns, keeping your elbows pinned to your sides.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Overhead Triceps Extension",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Anchor the band beneath your feet, grasp it with both hands behind your neck, and extend your arms overhead.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Triceps Kickback",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Step on the band with one foot, lean forward, hold the band with one hand, and perform a kickback motion.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single-Arm Band Triceps Extension",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Anchor the band at a high point, grasp the end with one hand, and perform triceps extensions, isolating one arm at a time.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Lying Triceps Extension",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back, anchor the band under your feet, and perform triceps extensions overhead, mimicking skull crushers.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Overhead Triceps Extension",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a weight plate with both hands overhead. Bend your elbows to lower the plate behind your head, then extend your arms to lift the plate back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Triceps Press",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and hold a weight plate over your chest with both hands. Press the plate straight up until your arms are fully extended, then lower back down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Kickback",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Bend forward at the waist, hold a weight plate in one hand, and perform a kickback motion by extending your arm behind you.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Standing Plate Triceps Extension",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet shoulder-width apart, hold a weight plate with both hands behind your head, and extend your arms to press the plate overhead.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Behind-the-Back Lift",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a weight plate with both hands behind your back at waist level. Lift the plate up by extending your arms and flexing your triceps, then lower it back down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Diamond Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Get into a push-up position but with your hands close together under your chest, forming a diamond shape with your fingers to target the triceps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Dips",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Place your hands on a bench or chair behind you, extend your legs forward, and lower your body by bending your elbows before pushing back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close-Grip Push-ups",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Perform a push-up with your hands placed closer than shoulder-width apart to increase the focus on the triceps.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Triceps Plank Extensions",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with your forearms on the ground. Push up off your forearms, extending your arms to engage your triceps, then lower back into the plank.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Triceps Body Press",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand facing away from a wall, place your hands on the ground, and walk your feet up the wall. Bend your elbows to lower your head towards the ground, then push back up.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Russian Twist",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the floor with your knees bent, holding a kettlebell with both hands. Lean back slightly and twist your torso to move the kettlebell from one side of your body to the other.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Windmill",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet shoulder-width apart, holding a kettlebell overhead with one arm. Bend at the waist and lower your free hand towards the opposite foot, then return to the starting position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Plank Drag",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with a kettlebell outside one arm. Reach under your torso with the opposite arm to drag the kettlebell to the other side, and repeat.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Sit-up",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your knees bent, holding a kettlebell on your chest. Perform a sit-up, keeping the kettlebell close to your chest.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Hanging Leg Raise",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a pull-up bar holding a kettlebell with your feet, raise your legs to parallel to the ground, then lower them back down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Side Bend",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with feet hip-width apart, holding a dumbbell in one hand. Bend to the side with the dumbbell, then return to the upright position. Repeat on both sides.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Wood Chop",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet wider than hip-width apart, holding a dumbbell with both hands. Twist your torso, and swing the dumbbell diagonally across the body like chopping wood.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Plank Pull-through",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Start in a high plank position with a dumbbell on the floor next to your outside hand. Reach under your torso with the opposite hand to grab the dumbbell and drag it to the other side. Repeat the pull-through while maintaining plank position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Weighted Russian Twist",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the floor with knees bent, holding a dumbbell with both hands. Lean back slightly and lift your feet off the floor. Rotate your torso to move the dumbbell from one side of your body to the other.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Dead Bug",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with arms extended holding a dumbbell above your chest. Lift your legs so your knees are above your hips. Slowly lower opposite arm and leg towards the floor, then return to the start position and repeat with the other arm and leg.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Rollout",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Kneel on the floor with a barbell in front of you. Grip the barbell with both hands, roll the barbell forward, extending your body as straight as possible, then pull back to the starting position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Landmine 180s",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Secure one end of a barbell in a corner (or landmine station), lift the other end with both hands. Twist your torso to swing the barbell end from one side to the other in a semicircular motion.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Side Bend",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Stand up straight holding a barbell on your shoulders. Bend at the waist to one side as far as possible, then stand straight again. Repeat on the other side.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Ab Rollout (Standing)",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet shoulder-width apart and a barbell in front of you. Lean forward and roll the barbell away from you until your body is extended, then roll it back towards your feet.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Overhead Barbell Oblique Crunch",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet shoulder-width apart, holding a barbell overhead. Keep your arms straight and bend at the waist to one side, then return to the center before bending to the other side.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hanging Leg Raise",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a pull-up bar with your legs straight down. Raise your legs to form an L-shape with your body, then lower them back down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hanging Knee Raise Twist",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a pull-up bar and raise your knees towards your chest. " +
                              "Twist your torso as you lift your knees to work the ",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Toes to Bar",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a bar and with straight legs, raise your toes up to touch the bar, then lower back down in a controlled manner.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Windshield Wipers",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a bar, raise your legs to 90 degrees, then rotate your legs from side to side like windshield wipers.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hanging Scissor Kicks",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a pull-up bar and perform a scissor motion with your legs, alternating each leg up and down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Kneeling Crunch",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Kneel on the floor with the band anchored above you. Hold the ends of the band, crunch your abdomen, bringing your hands towards the floor.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Woodchoppers",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Anchor the band at a high point, stand sideways to the anchor, and hold the band with both hands. Rotate your torso and pull the band down across your body to your opposite knee.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band PallofPress",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Anchor the band at chest height, stand perpendicular to the anchor point holding the band with both hands close to your chest. Extend your arms straight out in front of your chest, then bring them back in. Keep your torso steady and resist the pull of the band.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Twist",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Anchor the band at waist height, stand with your side to the anchor and hold the band with both hands. Rotate your torso away from the anchor point, stretching the band as you twist. Return to the starting position.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Standing Abdominal Rollout",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet hip-width apart, anchor the band under your feet, and hold it with both hands in front of you. Lean forward, rolling your hands out away from your body, then pull back to stand straight.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Crunch",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with knees bent and feet flat on the floor. Hold a weight plate on your chest and crunch up, lifting your shoulders off the floor.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Russian Twist",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the floor with knees bent, lean back slightly. Hold a weight plate with both hands and twist your torso to move the plate from one side of your body to the other.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Standing Plate Twist",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with feet hip-width apart, holding a weight plate at chest level. Twist your torso, moving the plate to the left and then to the right.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Overhead Sit-up",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back, legs straight, and hold a weight plate above your head. Perform a sit-up, keeping the plate overhead throughout the movement.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Toe Touch",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with legs straight up. Hold a weight plate with both hands and crunch up, trying to touch your toes with the plate.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plank",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Get into a push-up position but rest on your forearms. Keep your body straight and hold this position to engage your core.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Mountain Climbers",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Start in a high plank position. Drive your knees towards your chest one at a time, like running in place.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Leg Raises",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back, legs straight, and lift your legs to 90 degrees without bending your knees. Lower them back down without touching the floor.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bicycle Crunches",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your hands behind your head and legs raised and bent at 90 degrees. Alternate sides by bringing your right elbow towards your left knee and then your left elbow towards your right knee, mimicking a cycling motion.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });
            _context.Exercises.Add(new Exercise()
            {
                Name = "Bicycle Crunches",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your hands behind your head and legs raised and bent at 90 degrees. Alternate sides by bringing your right elbow towards your left knee and then your left elbow towards your right knee, mimicking a cycling motion.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });



            _context.Exercises.Add(new Exercise()
            {
                Name = "V-Ups",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie flat on your back with your arms extended above your head. Lift your legs and upper body at the same time, trying to touch your toes at the top of the movement, then lower back down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Squat",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell by the horns close to your chest. Squat down by bending at the knees and pushing your hips back, then return to standing.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Lunges",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in each hand at your sides. Step forward into a lunge, lowering your back knee towards the ground, then push back up to standing. Alternate legs.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Deadlift",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet hip-width apart with a kettlebell between them. Bend at the hips to grab the kettlebell, keep your back straight, stand up by extending your hips, then lower the kettlebell back down.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single-Leg Kettlebell Deadlift",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand on one leg with a kettlebell in front of you. Bend forward at the hip, extending your free leg behind you for balance, and grab the kettlebell. Return to standing.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Swing",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with feet slightly wider than hip-width apart, holding a kettlebell with both hands. Bend your knees slightly, hinge at your hips to swing the kettlebell between your legs, then thrust your hips forward to swing the kettlebell up to chest height.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Squat",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with feet hip-width apart, holding a dumbbell in each hand at your sides. Squat down by bending your knees and pushing your hips back, then drive through your heels to return to standing.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Lunges",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand at your sides. Step forward into a lunge, lowering your hips until both knees are bent at about a 90-degree angle. Push back up to the starting position and alternate legs.",
                YoutubeUrl = "https://www.youtube.com/watch?v=exampleURL"
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


