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
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                Description = "Swing the kettlebell between your legs and up to chest level",
                YoutubeUrl = "https://www.youtube.com/watch?v=YSxHifyI6s8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Row",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Bend forward and pull the kettlebell towards your waist",
                YoutubeUrl = "https://www.youtube.com/watch?v=u4sG54-HKJw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Deadlift",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                Description = "Lift the kettlebell from the ground to a standing position",
                YoutubeUrl = "https://www.youtube.com/watch?v=-8JbTKR50rk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Single-Arm Kettlebell Row",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Teres Major"),
                Description = "Bend forward and pull the kettlebell with one arm",
                YoutubeUrl = "https://www.youtube.com/watch?v=pYcpY20QaE8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell High Pull",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Traps"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Pull the kettlebell up to your shoulder, leading with your elbow",
                YoutubeUrl = "https://www.youtube.com/watch?v=0B5JwVgFLP4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Pull the dumbbell towards your hip while supporting yourself on a bench",
                YoutubeUrl = "https://www.youtube.com/watch?v=pYcpY20QaE8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Deadlift",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                Description = "Lift the dumbbells from the ground to a standing position",
                YoutubeUrl = "https://www.youtube.com/watch?v=ytGaGIn3SjE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Pullover",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Serratus Posterior"),
                Description = "Lie on a bench and extend a dumbbell over your chest, then lower it behind your head",
                YoutubeUrl = "https://www.youtube.com/watch?v=0G2_XV7slIg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Renegade Row",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                Description = "In a plank position, row a dumbbell with one hand, then alternate",
                YoutubeUrl = "https://www.youtube.com/watch?v=Zo61jP2O3Ug"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Shrug",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Traps"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Levator Scapulae"),
                Description = "Lift your shoulders up towards your ears while holding dumbbells",
                YoutubeUrl = "https://www.youtube.com/watch?v=cJRVVxmytaM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Bend over and pull the barbell towards your waist",
                YoutubeUrl = "https://www.youtube.com/watch?v=G8l_8chR5BE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Deadlift",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                Description = "Lift the barbell from the ground to a standing position",
                YoutubeUrl = "https://www.youtube.com/watch?v=3UwO0fKukRw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Shrug",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Traps"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Levator Scapulae"),
                Description = "Shrug your shoulders while holding a barbell",
                YoutubeUrl = "https://www.youtube.com/watch?v=cJRVVxmytaM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "T-Bar Row",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Row a T-bar loaded with weight, pulling towards your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=j3Igk5nyZE4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Good Morning",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                Description = "Bend at your waist with a barbell on your back",
                YoutubeUrl = "https://www.youtube.com/watch?v=PLHY2-nt-y4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Bend forward and pull the plate towards your waist",
                YoutubeUrl = "https://www.youtube.com/watch?v=example-url"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Shrug",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Traps"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Levator Scapulae"),
                Description = "Lift your shoulders up towards your ears while holding a plate",
                YoutubeUrl = "https://www.youtube.com/watch?v=example-url"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bent Over Plate Row",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Bend at the waist and row the plate towards your stomach",
                YoutubeUrl = "https://www.youtube.com/watch?v=example-url"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Standing Plate Twist",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Obliques"),
                Description = "Hold the plate in front of you and twist your torso left and right",
                YoutubeUrl = "https://www.youtube.com/watch?v=example-url"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Deadlift",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                Description = "Lift the plate from the ground to a standing position",
                YoutubeUrl = "https://www.youtube.com/watch?v=example-url"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Wide Grip Pull-Up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Pull yourself up with a wide grip",
                YoutubeUrl = "https://www.youtube.com/watch?v=eGo4IYlbE5g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Chin-Up",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Biceps"),
                Description = "Pull yourself up with your palms facing you",
                YoutubeUrl = "https://www.youtube.com/watch?v=brhRXlOhsAM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Neutral Grip Pull-Up",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Pull yourself up with a neutral grip",
                YoutubeUrl = "https://www.youtube.com/watch?v=sfTsgt-a63c"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hanging Leg Raise",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lower Abs"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Hip Flexors"),
                Description = "Hang from the bar and raise your legs to 90 degrees",
                YoutubeUrl = "https://www.youtube.com/watch?v=hdng3Nm1x_E"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Behind The Neck Pull-Up",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Teres Major"),
                Description = "Pull yourself up so that the bar touches the back of your neck",
                YoutubeUrl = "https://www.youtube.com/watch?v=A16zIcbR5sU"
            });



            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Pullover on Bench",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Serratus Anterior"),
                Description = "Lie on a bench and extend a dumbbell over your chest, then lower it behind your head",
                YoutubeUrl = "https://www.youtube.com/watch?v=0G2_XV7slIg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Row",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                Description = "Lay face down on a bench and row dumbbells towards your hips",
                YoutubeUrl = "https://www.youtube.com/watch?v=kBWAon7ItDw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Dumbbell Row",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Lay face down on an incline bench and row dumbbells",
                YoutubeUrl = "https://www.youtube.com/watch?v=leQ4uoAxVLE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Hyperextension",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Glutes"),
                Description = "Lay stomach down on a bench with your hips at the end, then raise and lower your upper body",
                YoutubeUrl = "https://www.youtube.com/watch?v=qtjJUWCnDyE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Seated Band Row",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                Description = "Sit on a bench with a band wrapped around your feet and row towards your waist",
                YoutubeUrl = "https://www.youtube.com/watch?v=GZbfZ033f74"
            });



            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Pull Apart",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rear Delts"),
                Description = "Hold a band in front of you and pull it apart horizontally",
                YoutubeUrl = "https://www.youtube.com/watch?v=JObYtU7Y7ag"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Banded Row",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Anchor the band and pull it towards your waist",
                YoutubeUrl = "https://www.youtube.com/watch?v=rloXYB8M3vU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Banded Lat Pulldown",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Teres Major"),
                Description = "Anchor the band overhead and pull down towards your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=CAwf7n6Luuc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Banded Deadlift",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Glutes"),
                Description = "Stand on the band and perform a deadlift motion",
                YoutubeUrl = "https://www.youtube.com/watch?v=lFqAijL2rJ4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Banded Good Morning",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Hamstrings"),
                Description = "Place the band over your neck and perform a good morning motion",
                YoutubeUrl = "https://www.youtube.com/watch?v=O31MmhW72WE"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Bodyweight Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Find a low bar or rings, lie underneath it and pull yourself up",
                YoutubeUrl = "https://www.youtube.com/watch?v=dvkIaarnf0g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Superman",
                Priority = 2,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Glutes"),
                Description = "Lie on your stomach and lift your arms and legs off the ground",
                YoutubeUrl = "https://www.youtube.com/watch?v=cc6UVRS7PW4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Reverse Snow Angels",
                Priority = 3,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rear Delts"),
                Description = "Lie on your stomach and move your arms from your hips to over your head",
                YoutubeUrl = "https://www.youtube.com/watch?v=2VmprBYtCug"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Inverted Row",
                Priority = 4,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Lats"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Rhomboids"),
                Description = "Find a bar at waist height, lie underneath it, and pull your chest towards the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=dvkIaarnf0g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Back Extension",
                Priority = 5,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscle = _context.Muscles.Single(e => e.Name == "Erector Spinae"),
                SecondaryMuscle = _context.Muscles.Single(e => e.Name == "Glutes"),
                Description = "Lie face down on the ground and lift your upper body off the floor",
                YoutubeUrl = "https://www.youtube.com/watch?v=ph3pddpKzzw"
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


