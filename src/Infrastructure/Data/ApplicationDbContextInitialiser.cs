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
                Name = "Shoulders",
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
                Name = "Abs",
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
                Name = "Legs",
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
                Name = "Biceps",
                Description = "Muscles of the forearm responsible for wrist and finger movements.",
                MuscleGroup = _context.MuscleGroups.Single(mg => mg.Id == 2)
            });

            _context.Muscles.Add(new Muscle
            {
                Name = "Legs",
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
                Name = "Internal Abs",
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
                Name = "Kettlebell Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hold the kettlebell in one hand and row it towards your hip",
                YoutubeUrl = "https://www.youtube.com/watch?v=vQRlGlNnKDw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Chest Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and press the kettlebell from your chest to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=ktLKtjNdM9o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the kettlebell overhead with both hands and lower it behind your head",
                YoutubeUrl = "https://www.youtube.com/watch?v=2L3ZO9XlRBk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Press the kettlebell overhead from shoulder height to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=9nGEt6vUIh8"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Goblet Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell at chest level and perform squats",
                YoutubeUrl = "https://www.youtube.com/watch?v=MjdpR-TY6hA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in each hand and perform lunges",
                YoutubeUrl = "https://www.youtube.com/watch?v=qTNWGCGJNP0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Step-Ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in each hand and step up onto a platform",
                YoutubeUrl = "https://www.youtube.com/watch?v=Wp7TkP8ZBgs"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hold a kettlebell between your legs and perform deadlifts",
                YoutubeUrl = "https://www.youtube.com/watch?v=YV8lZTOg0SM"
            });

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
                Name = "Kettlebell Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hold the kettlebell in one hand and row it towards your hip",
                YoutubeUrl = "https://www.youtube.com/watch?v=vQRlGlNnKDw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Hold a kettlebell between your legs and perform deadlifts",
                YoutubeUrl = "https://www.youtube.com/watch?v=YV8lZTOg0SM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Swings",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Swing the kettlebell between your legs and up to chest level",
                YoutubeUrl = "https://www.youtube.com/watch?v=YSxHifyI6s8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Clean and Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Clean the kettlebell to shoulder height and then press it overhead",
                YoutubeUrl = "https://www.youtube.com/watch?v=2z7CFI25HkQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Renegade Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Get into a plank position with one hand on the kettlebell, row the kettlebell to your side and alternate arms",
                YoutubeUrl = "https://www.youtube.com/watch?v=2EJFNuGI2N8"
            });



            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Chest Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and press the kettlebell from your chest to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=ktLKtjNdM9o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and perform flyes with the kettlebell",
                YoutubeUrl = "https://www.youtube.com/watch?v=vQxDra1jBJM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Pullovers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lie on your back and hold the kettlebell overhead, lower it behind your head and then bring it back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=-_r-MixuSho"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Perform push-ups with your hands on kettlebells",
                YoutubeUrl = "https://www.youtube.com/watch?v=MRU5KsyYv5A"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Chest Squeeze Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Hold two kettlebells together in front of your chest and press them together",
                YoutubeUrl = "https://www.youtube.com/watch?v=h9TJWu8VN3k"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the kettlebell overhead with both hands and lower it behind your head",
                YoutubeUrl = "https://www.youtube.com/watch?v=2L3ZO9XlRBk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Skull Crushers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and lower the kettlebell towards your forehead, then extend your arms back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=K2gk9LGFa6A"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Overhead Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the kettlebell with both hands overhead and lower it behind your head, then extend your arms back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=SG4JtvPlp4Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Kickbacks",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Bend over at the waist and extend the kettlebell behind you with one arm",
                YoutubeUrl = "https://www.youtube.com/watch?v=UiDv3hNcvHk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Close Grip Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Lie on your back and hold the kettlebell with both hands close to your chest, then extend your arms up",
                YoutubeUrl = "https://www.youtube.com/watch?v=JItNYoBIbWM"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Press the kettlebell overhead from shoulder height to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=9nGEt6vUIh8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Lateral Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in each hand and raise them out to the sides to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=W66xPqNRn7E"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Front Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in each hand and raise them in front of you to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=ZlvKIkgHJlY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell High Pulls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Pull the kettlebell up to shoulder height with elbows high",
                YoutubeUrl = "https://www.youtube.com/watch?v=WLYrY6vIxsg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Upright Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hold a kettlebell in each hand and raise them up under your chin",
                YoutubeUrl = "https://www.youtube.com/watch?v=BGMjcDgqNX0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Bicep Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in each hand with arms straight, curl the weights up to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=-lZu4l7t6E0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Hammer Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in each hand with palms facing each other, curl the weights up to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=9kiSe1URJ8A"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Preacher Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench and place your elbows on the inside of your knees, curl the kettlebell up to your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=ftfJ0ZqA1V8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Concentration Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench and hold the kettlebell with one hand, curl the weight up towards your shoulder",
                YoutubeUrl = "https://www.youtube.com/watch?v=5_Lqkgj2-m0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Drag Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell in each hand with palms facing backwards, curl the weights up to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=75tmOYlGU3Q"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Russian Twists with Kettlebell",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the floor, hold a kettlebell with both hands, and twist your torso from side to side",
                YoutubeUrl = "https://www.youtube.com/watch?v=0bnclWtiIIs"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Windmills",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell overhead with one arm and touch your opposite foot with the free hand",
                YoutubeUrl = "https://www.youtube.com/watch?v=OqnszD0AhcU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Turkish Get-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back, hold a kettlebell overhead, and perform a series of movements to stand up and then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=0bWRPC49-KI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Plank Drags",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Get into a plank position with one hand on a kettlebell, drag the kettlebell to the other side and alternate arms",
                YoutubeUrl = "https://www.youtube.com/watch?v=6N71K8LbR7Q"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Sit-up and Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Perform a sit-up while holding a kettlebell and press the kettlebell overhead at the top of the movement",
                YoutubeUrl = "https://www.youtube.com/watch?v=4RQzdJg7Oqs"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Turkish Get-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back, hold a kettlebell overhead, and perform a series of movements to stand up and then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=0bWRPC49-KI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Swings",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Swing the kettlebell between your legs and up to chest level",
                YoutubeUrl = "https://www.youtube.com/watch?v=YSxHifyI6s8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Goblet Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold a kettlebell at chest level and perform squats",
                YoutubeUrl = "https://www.youtube.com/watch?v=MjdpR-TY6hA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Renegade Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Get into a plank position with one hand on the kettlebell, row the kettlebell to your side and alternate arms",
                YoutubeUrl = "https://www.youtube.com/watch?v=2EJFNuGI2N8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kettlebell Clean and Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Kettlebell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Clean the kettlebell to shoulder height and then press it overhead",
                YoutubeUrl = "https://www.youtube.com/watch?v=2z7CFI25HkQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand and perform squats",
                YoutubeUrl = "https://www.youtube.com/watch?v=Q5eUgpQUtPc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand and perform lunges",
                YoutubeUrl = "https://www.youtube.com/watch?v=QOVaHwm-Q6U"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Step-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand and step up onto a platform",
                YoutubeUrl = "https://www.youtube.com/watch?v=Cd0c63OF6jc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hold a dumbbell in each hand and perform deadlifts",
                YoutubeUrl = "https://www.youtube.com/watch?v=5mXLx_fo0CA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Bulgarian Split Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand and perform split squats with one foot elevated",
                YoutubeUrl = "https://www.youtube.com/watch?v=2C-uNgKwPLE"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hold a dumbbell in one hand and row it towards your hip",
                YoutubeUrl = "https://www.youtube.com/watch?v=wvAuiMrG4wQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Hold a dumbbell in each hand and perform deadlifts",
                YoutubeUrl = "https://www.youtube.com/watch?v=5mXLx_fo0CA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Renegade Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Get into a plank position with one hand on a dumbbell, row the dumbbell to your side and alternate arms",
                YoutubeUrl = "https://www.youtube.com/watch?v=dwh-yzjaaZ0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Pullovers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Lie on your back and hold a dumbbell overhead, lower it behind your head and then bring it back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=HQmF8k6f7ys"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Hold a dumbbell in each hand and shrug your shoulders upwards",
                YoutubeUrl = "https://www.youtube.com/watch?v=zRCM5pCE1Mo"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench and press the dumbbells from your chest to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=rra7u4Yf6jk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench and perform flyes with the dumbbells",
                YoutubeUrl = "https://www.youtube.com/watch?v=Iqu4ROFGWC4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Dumbbell Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on an incline bench and press the dumbbells from your chest to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=Hd1rK4G1sI8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Pullovers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Lie on your back and hold a dumbbell overhead, lower it behind your head and then bring it back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=HQmF8k6f7ys"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Chest Squeeze Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Hold two dumbbells together in front of your chest and press them together",
                YoutubeUrl = "https://www.youtube.com/watch?v=7zMbqPnczJg"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Tricep Kickbacks",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand, lean forward, and extend your arms back",
                YoutubeUrl = "https://www.youtube.com/watch?v=lK7IzptwoWQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell with both hands overhead and lower it behind your head",
                YoutubeUrl = "https://www.youtube.com/watch?v=VcYCbB4o3SQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Skull Crushers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and lower the dumbbells towards your forehead, then extend your arms back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=2-LAMcpzODU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Close Grip Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Hold dumbbells close together and perform a bench press",
                YoutubeUrl = "https://www.youtube.com/watch?v=HNqoRP1hZCI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Overhead Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell with both hands overhead and extend your arms up",
                YoutubeUrl = "https://www.youtube.com/watch?v=JEZmmVAVl1M"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Press the dumbbells overhead from shoulder height to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=7MGlzDcWvuE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Lateral Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand and raise them out to the sides to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=EmvNOLP7lqo"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Front Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand and raise them in front of you to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=9VTJzVUvzdg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Arnold Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold dumbbells at shoulder height with palms facing you, press them overhead and twist your palms away at the top",
                YoutubeUrl = "https://www.youtube.com/watch?v=vj2w851ZHRM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hold a dumbbell in each hand and shrug your shoulders upwards",
                YoutubeUrl = "https://www.youtube.com/watch?v=I27ybOmjV0A"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Russian Twists",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the floor, hold a dumbbell with both hands, and twist your torso from side to side",
                YoutubeUrl = "https://www.youtube.com/watch?v=5kwW0N8nx8s"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Side Bends",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Stand upright and hold a dumbbell in one hand, bend to the side",
                YoutubeUrl = "https://www.youtube.com/watch?v=K_1BHqzXNCM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Woodchoppers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell with both hands and perform a diagonal chopping motion",
                YoutubeUrl = "https://www.youtube.com/watch?v=Gz4fOUeyxVs"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Sit-up and Twist",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in both hands, perform a sit-up, and twist your torso at the top",
                YoutubeUrl = "https://www.youtube.com/watch?v=9SGg6Qn4WJY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Leg Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back, hold a dumbbell between your feet, and raise your legs up and down",
                YoutubeUrl = "https://www.youtube.com/watch?v=-gSSQJAUncU"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Thrusters",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand at shoulder height, squat down, and then press the dumbbells overhead as you stand up",
                YoutubeUrl = "https://www.youtube.com/watch?v=c65quxVtYrM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Renegade Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Get into a plank position with one hand on a dumbbell, row the dumbbell to your side and alternate arms",
                YoutubeUrl = "https://www.youtube.com/watch?v=dwh-yzjaaZ0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Burpees",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold dumbbells in each hand, squat down, kick your feet back into a push-up position, return to squat position, and stand up while pressing the dumbbells overhead",
                YoutubeUrl = "https://www.youtube.com/watch?v=GvibHzbzXao"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Walking Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold a dumbbell in each hand and perform walking lunges",
                YoutubeUrl = "https://www.youtube.com/watch?v=hCm7g0-CIoc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Clean and Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Dumbbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Clean the dumbbells to shoulder height and then press them overhead",
                YoutubeUrl = "https://www.youtube.com/watch?v=2z7CFI25HkQ"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Place a barbell on your upper back, squat down, and then stand back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=Dy28eq2PjcM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a barbell across your upper back and perform lunges",
                YoutubeUrl = "https://www.youtube.com/watch?v=QOVaHwm-Q6U"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Stand with a barbell on the floor, bend at the hips and knees to lower down, and then stand back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=-4qRntuXBSc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Step-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a barbell across your upper back and step up onto a platform",
                YoutubeUrl = "https://www.youtube.com/watch?v=MBJtxg8sBb8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Bulgarian Split Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold a barbell across your upper back and perform split squats with one foot elevated",
                YoutubeUrl = "https://www.youtube.com/watch?v=1xMaFsW1Qx4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Bent Over Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hold a barbell with an overhand grip, bend forward at the waist, and row the barbell towards your lower chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=G8l_8chR5BE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Stand with a barbell on the floor, bend at the hips and knees to lower down, and then stand back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=-4qRntuXBSc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Pull-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Attach a barbell to a pull-up bar and perform pull-ups",
                YoutubeUrl = "https://www.youtube.com/watch?v=eGo4IYlbE5g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Hold a barbell with an overhand grip and shrug your shoulders upwards",
                YoutubeUrl = "https://www.youtube.com/watch?v=C9UWujtiACs"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Good Mornings",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Hold a barbell across your upper back, bend forward at the hips while keeping your back straight, and then return to standing",
                YoutubeUrl = "https://www.youtube.com/watch?v=uHcXPcma09k"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench and lower the barbell to your chest, then press it back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=VmB1G1K7v94"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Incline Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on an incline bench and lower the barbell to your upper chest, then press it back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=GUcb7X1F5SU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Decline Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a decline bench and lower the barbell to your lower chest, then press it back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=nhpWETtkFg8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Chest Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on the floor and press the barbell up from your chest to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=qyG2-NE76WI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Floor Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on the floor and press the barbell up from your chest to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=shsA2GivvGE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close Grip Barbell Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Perform a bench press with a narrow grip to target the triceps",
                YoutubeUrl = "https://www.youtube.com/watch?v=fVmcD8pGJpk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the barbell with an overhand grip above your head, lower it behind your head, and then extend your arms",
                YoutubeUrl = "https://www.youtube.com/watch?v=icFfvck0fBg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Skull Crushers (Barbell)",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench and lower the barbell towards your forehead, then extend your arms to press it back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=IzTVG5ufqfU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Overhead Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the barbell with both hands overhead and extend your arms up",
                YoutubeUrl = "https://www.youtube.com/watch?v=JqHbgIXnGzI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Tricep Dips",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Use parallel bars to perform dips, keeping your elbows close to your body to target the triceps",
                YoutubeUrl = "https://www.youtube.com/watch?v=4dF1DOWzf20"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Overhead Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Press the barbell overhead from shoulder height to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=CnBmiBqp-AI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Upright Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Hold the barbell with an overhand grip and raise it towards your chin, keeping it close to your body",
                YoutubeUrl = "https://www.youtube.com/watch?v=SJrMCOkWbKU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Front Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold the barbell with an overhand grip and raise it in front of you to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=0tT2zokJLW4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hold the barbell with an overhand grip and shrug your shoulders upwards",
                YoutubeUrl = "https://www.youtube.com/watch?v=C9UWujtiACs"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Push Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Perform a slight dip with your legs and then explosively press the barbell overhead",
                YoutubeUrl = "https://www.youtube.com/watch?v=nvcqaUed910"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Rollouts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Kneel on the floor, hold the barbell with both hands, and roll it forward until your body is fully extended, then roll it back",
                YoutubeUrl = "https://www.youtube.com/watch?v=0dMzSIlD_-8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Russian Twists",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the floor, hold the barbell with both hands, and rotate your torso from side to side",
                YoutubeUrl = "https://www.youtube.com/watch?v=gdcstKSGI8Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Sit-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back, hold the barbell on your chest, and perform sit-ups",
                YoutubeUrl = "https://www.youtube.com/watch?v=9VeO2GfcSuI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Leg Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a barbell and raise your legs up towards your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=JB2oyawG9KI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Lying Twists",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your legs bent, hold the barbell above your chest, and twist your torso from side to side",
                YoutubeUrl = "https://www.youtube.com/watch?v=0xqfuL2ooDw"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Squat and Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold the barbell at shoulder height, squat down, and then press the barbell overhead as you stand up",
                YoutubeUrl = "https://www.youtube.com/watch?v=c65quxVtYrM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Deadlift and Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Perform a deadlift, then bend forward and row the barbell towards your lower chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=PGO5KF3BNnM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Clean and Jerk",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Clean the barbell to shoulder height and then jerk it overhead in one explosive motion",
                YoutubeUrl = "https://www.youtube.com/watch?v=2z7CFI25HkQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Thrusters",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold the barbell at shoulder height, squat down, and then press the barbell overhead as you stand up",
                YoutubeUrl = "https://www.youtube.com/watch?v=Y2PIgEo_Rd4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Lunges with Rotation",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Barbell"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold the barbell overhead and perform lunges, rotating your torso to the side as you lunge",
                YoutubeUrl = "https://www.youtube.com/watch?v=5jD3fHDtQ2Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate at chest height and squat down, then stand back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=xDddiEwB6Uk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate overhead or at chest height and perform lunges",
                YoutubeUrl = "https://www.youtube.com/watch?v=0D6qZcKADv0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Step-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate at chest height and step onto a platform, then step back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=UW1r1Sb4E_Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Calf Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate in your hands and perform calf raises",
                YoutubeUrl = "https://www.youtube.com/watch?v=hyv14e2QDq0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Sumo Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate at chest height and perform sumo squats",
                YoutubeUrl = "https://www.youtube.com/watch?v=UTfVVaxKM9E"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Bent Over Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hold the plate with both hands and bend forward at the waist, then row the plate towards your lower chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=LfjOcpkHfRQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Hold the plate with both hands and stand up with a straight back, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=1Jl2MfiQcEw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Pullovers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Lie on a bench and hold the plate with both hands overhead, then lower it behind your head and back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=tcl4TYnn0rI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Renegade Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Get into a plank position with both hands on the plate, row one arm up towards your chest, then alternate",
                YoutubeUrl = "https://www.youtube.com/watch?v=Og0z3P-USts"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Hold the plate with both hands and shrug your shoulders upwards",
                YoutubeUrl = "https://www.youtube.com/watch?v=FxYEcckvAeA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and press the plate upwards from your chest to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=qK9XJdZUv8E"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Chest Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and hold the plate with slightly bent arms, then open your arms out to the sides and bring them back together",
                YoutubeUrl = "https://www.youtube.com/watch?v=v7VB4aFd7fQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Push-Ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Perform push-ups with your hands on the plate",
                YoutubeUrl = "https://www.youtube.com/watch?v=sBNd-cwE7WY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Squeeze Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on your back and hold the plate with both hands, then press the plate together as hard as possible",
                YoutubeUrl = "https://www.youtube.com/watch?v=wwe0GwpRu88"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and hold the plate with both hands, then press it upwards from your chest to full extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=dqucuyHBVOk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate overhead with both hands and lower it behind your head, then extend your arms to raise it back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=bJ_RPAmN4nU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Skull Crushers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back and hold the plate with both hands, then lower it towards your forehead and extend your arms to raise it back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=2yjwXTZQDDI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Overhead Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate overhead with both hands and extend your arms upwards",
                YoutubeUrl = "https://www.youtube.com/watch?v=8AdBJg2xoDI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Tricep Dips",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Perform dips with your hands on the plate",
                YoutubeUrl = "https://www.youtube.com/watch?v=glbve8G3f5o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Tricep Kickbacks",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate with one hand and extend your arm backwards, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=4Yc0rOQ2Yi0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Front Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate with both hands and raise it in front of you to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=mg91xw8zokY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Lateral Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate with both hands at your sides and raise it out to the sides to shoulder height",
                YoutubeUrl = "https://www.youtube.com/watch?v=rnrAUFJl4Gs"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate with both hands at shoulder height and press it overhead",
                YoutubeUrl = "https://www.youtube.com/watch?v=qEwKCR5JCog"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Shoulder Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate with both hands at your sides and shrug your shoulders upwards",
                YoutubeUrl = "https://www.youtube.com/watch?v=0Z6yf5-ntU8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Arnold Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate with both hands and start with the plate in front of you, then press it overhead while rotating your hands",
                YoutubeUrl = "https://www.youtube.com/watch?v=6Z15_WdXm5E"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate with both hands and curl it towards your shoulders",
                YoutubeUrl = "https://www.youtube.com/watch?v=tojvzGWnn3Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Hammer Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate with both hands and curl it towards your shoulders with a neutral grip",
                YoutubeUrl = "https://www.youtube.com/watch?v=w6ntZ7Mjw-0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Preacher Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Rest your arms on a preacher bench and hold the plate with an underhand grip, then curl it towards your shoulders",
                YoutubeUrl = "https://www.youtube.com/watch?v=JpUzkbZ7Tr8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Reverse Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hold the plate with both hands and curl it towards your shoulders with an overhand grip",
                YoutubeUrl = "https://www.youtube.com/watch?v=TMcWwBxyq_4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Concentration Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench with your elbow resting on your thigh and hold the plate with one hand, then curl it towards your shoulder",
                YoutubeUrl = "https://www.youtube.com/watch?v=Jm1CW9YqCb0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Woodchoppers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate with both hands and rotate your torso as you bring the plate from one side of your body to the other",
                YoutubeUrl = "https://www.youtube.com/watch?v=4xfHrB3azTk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Russian Twists",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the floor and hold the plate with both hands, then rotate your torso from side to side",
                YoutubeUrl = "https://www.youtube.com/watch?v=DQ8PnZsDHik"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Overhead Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate overhead with both hands and perform lunges",
                YoutubeUrl = "https://www.youtube.com/watch?v=ZX4ZPjk2ztw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Squat Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate at chest height and squat down, then press the plate overhead as you stand up",
                YoutubeUrl = "https://www.youtube.com/watch?v=wHlj5vkAHTo"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plate Burpees",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Plate"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Hold the plate in your hands and perform a burpee, jumping with the plate overhead",
                YoutubeUrl = "https://www.youtube.com/watch?v=k9v9lxjR0LY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hang from the bar with an overhand grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=eGo4IYlbE5g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Chin-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hang from the bar with an underhand grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=6kALZikXxLc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Wide Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hang from the bar with a wider than shoulder-width grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=QvWxw3HjFWA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Narrow Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hang from the bar with a shoulder-width grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=sb1VpPtoQ4Q"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Commando Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hang from the bar with one hand facing forward and the other facing backward, then pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=mJScZaL3ErM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Chin-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with an underhand grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=6kALZikXxLc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hammer Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with a neutral grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=jCg4I9JHInQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Towel Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Hang towels over the bar and grab them with an underhand grip, then pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=4P4bDoe-FIk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Mixed Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with one hand in an overhand grip and the other in an underhand grip, then pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=IP5E2xl87MM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with a narrow grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=BjzdRW7fgLI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Wide Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with a wider than shoulder-width grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=QvWxw3HjFWA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with a narrow grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=BjzdRW7fgLI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Archer Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with one hand and extend the other arm out to the side, then pull yourself up until your chin is above the bar while keeping the other arm straight",
                YoutubeUrl = "https://www.youtube.com/watch?v=brFMpUivIIo"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Weighted Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Attach a weight belt with added resistance and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=6kxGcNhOa-M"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Kipping Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Use a swinging motion to gain momentum and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=SR_c7m1zbl8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with a narrow grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=BjzdRW7fgLI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Neutral Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with a neutral grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=1Zg6aLR2S6o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Parallel Bar Dip",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Hold onto parallel bars and lower yourself down until your elbows are at 90-degree angles, then push yourself back up",
                YoutubeUrl = "https://www.youtube.com/watch?v=8xwog0HjjH4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Negative Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Jump up to the bar and slowly lower yourself down, focusing on the negative portion of the movement",
                YoutubeUrl = "https://www.youtube.com/watch?v=0p0o7XTbjN0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Tricep Pull-down",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to the bar and pull it down with straight arms, focusing on contracting the triceps",
                YoutubeUrl = "https://www.youtube.com/watch?v=0gZGMzzlZbM"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Pull-up with Knee Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Perform a pull-up while simultaneously raising your knees towards your chest, engaging the shoulders and core",
                YoutubeUrl = "https://www.youtube.com/watch?v=2_AqvoRRxIM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Commando Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with one hand facing forward and the other facing backward, then pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=mJScZaL3ErM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Wide Grip Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Hang from the bar with a wider than shoulder-width grip and pull yourself up until your chin is above the bar",
                YoutubeUrl = "https://www.youtube.com/watch?v=QvWxw3HjFWA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Behind the Neck Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Perform a pull-up while pulling yourself up towards the bar behind your neck",
                YoutubeUrl = "https://www.youtube.com/watch?v=n7n5wVTcWZs"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "L-sit Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Perform a pull-up while maintaining an L-sit position, engaging the shoulders and core",
                YoutubeUrl = "https://www.youtube.com/watch?v=v2KGmUw59-o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hanging Leg Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from the bar and raise your legs until they are parallel to the ground, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=JB2oyawG9KI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hanging Knee Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from the bar and raise your knees towards your chest, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=swjnuXoMe3E"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Windshield Wipers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Hang from the bar and rotate your legs from side to side while keeping them straight, mimicking the motion of windshield wipers",
                YoutubeUrl = "https://www.youtube.com/watch?v=2eUBaxO-QAw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Toes to Bar",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Hang from the bar and raise your legs until your toes touch the bar, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=y3niFzo5VLI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "L-sit Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Perform a pull-up while maintaining an L-sit position, engaging the abs and shoulders",
                YoutubeUrl = "https://www.youtube.com/watch?v=v2KGmUw59-o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Assisted Pull-up with Band",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to the bar and place one foot in it for assistance, then perform a pull-up",
                YoutubeUrl = "https://www.youtube.com/watch?v=kp4ymGZiAJY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Jumping Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                Description = "Jump up to the bar and pull yourself up until your chin is above the bar, using your legs for assistance",
                YoutubeUrl = "https://www.youtube.com/watch?v=DqMKSK6YdGI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Hanging Leg Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Hang from the bar and raise your legs until they are parallel to the ground, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=JB2oyawG9KI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Toes to Bar",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Hang from the bar and raise your legs until your toes touch the bar, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=y3niFzo5VLI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "L-sit Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Perform a pull-up while maintaining an L-sit position, engaging the legs and abs",
                YoutubeUrl = "https://www.youtube.com/watch?v=v2KGmUw59-o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Burpee Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Perform a burpee and then jump up to grab the bar and perform a pull-up",
                YoutubeUrl = "https://www.youtube.com/watch?v=h6ldL9Vd-j8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Muscle-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Perform a pull-up and transition into a dip at the top of the movement",
                YoutubeUrl = "https://www.youtube.com/watch?v=Q7I1JQVmYN8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "L-sit Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Perform a pull-up while maintaining an L-sit position, engaging the full body and abs",
                YoutubeUrl = "https://www.youtube.com/watch?v=v2KGmUw59-o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Behind the Neck Pull-up",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Perform a pull-up while pulling yourself up towards the bar behind your neck, engaging the full body and shoulders",
                YoutubeUrl = "https://www.youtube.com/watch?v=n7n5wVTcWZs"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Assisted Pull-up with Band",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Pull-up Bar"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Attach a resistance band to the bar and place one foot in it for assistance, then perform a pull-up",
                YoutubeUrl = "https://www.youtube.com/watch?v=kp4ymGZiAJY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Pull-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Lie face-up on a bench and grab the barbell from a rack, pull yourself up until your chest touches the bar, then lower yourself back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=78q_B0CGRIE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Bench Pull-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Lie face-up on an incline bench and grab the barbell from a rack, pull yourself up until your chest touches the bar, then lower yourself back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=_9yrDv57WO8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Bench Pull-overs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Lie face-up on a bench and hold a dumbbell with both hands, lower the weight behind your head until your arms are parallel to the ground, then pull the weight back over your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=heF1JJWjcOQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Barbell Rows on Bench",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Lie face-down on a bench with a barbell on the floor, grab the barbell with an overhand grip and row it up to your chest, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=ItVGp-q4IP4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Bench Supported Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Lie face-down on an incline bench and hold a dumbbell in each hand, row the weights up to your sides, squeezing your shoulder blades together at the top, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=g5D25DceJfU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Bicep Curls on Bench",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in each hand, curl the weights up towards your shoulders, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=ngmjvU7W4cQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Dumbbell Hammer Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie face-up on an incline bench with a dumbbell in each hand, curl the weights up with a neutral grip, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=3qjprkCJi-g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Zottman Curls on Bench",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in each hand, curl the weights up with a supinated grip, then rotate your wrists to a pronated grip and lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=FV0xmd4kU1k"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Concentration Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in one hand, rest your elbow on your inner thigh and curl the weight up towards your shoulder, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=cPkPwIetCDo"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Reverse Curl on Bench",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Sit on the edge of a bench with a barbell in front of you, grip the bar with an overhand grip and curl it up towards your shoulders, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=8YXw-3SqFsQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                Description = "Lie on a flat bench and grip the barbell slightly wider than shoulder-width, lower the bar to your chest, then press it back up to full arm extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=EHx1gYTA-Rw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on an incline bench with the backrest raised to about a 45-degree angle, grip the barbell slightly wider than shoulder-width, lower the bar to your upper chest, then press it back up to full arm extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=aoN0HCp7qLY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Decline Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on a decline bench with the backrest angled downward, grip the barbell slightly wider than shoulder-width, lower the bar to your lower chest, then press it back up to full arm extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=_QJtBdz1y5Q"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Dumbbell Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on a flat bench with a dumbbell in each hand, arms extended above your chest, lower the dumbbells out to the sides in a wide arc, then bring them back up to meet above your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=7rAPlxUvy5g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Dumbbell Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Lie on an incline bench with a dumbbell in each hand, arms extended above your chest, lower the dumbbells to your upper chest, then press them back up to full arm extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=JJPmiiFkQRU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close Grip Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Lie on a flat bench and grip the barbell with hands closer than shoulder-width apart, lower the bar to your chest, then press it back up to full arm extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=swA18JhsBwM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Tricep Dips on Bench",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Sit on the edge of a bench with your hands gripping the edge, slide your butt off the bench and lower yourself until your arms are at 90-degree angles, then push yourself back up to full arm extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=viEwU21-9eE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Tricep Kickbacks",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Kneel on the bench with a dumbbell in each hand, lean forward and extend your arms behind you, then flex your elbows to bring the weights back to your sides",
                YoutubeUrl = "https://www.youtube.com/watch?v=HsxX_HQRcl8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Tricep Dumbbell Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Lie on a flat bench with a dumbbell in each hand, arms extended above your chest, lower the dumbbells towards your shoulders, then press them back up to full arm extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=ALDsyH7XQzk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Dips",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Sit on the edge of a bench with your hands gripping the edge, slide your butt off the bench and lower yourself until your arms are at 90-degree angles, then push yourself back up to full arm extension",
                YoutubeUrl = "https://www.youtube.com/watch?v=0psQvVV3x24"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Seated Dumbbell Shoulder Press on Bench",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in each hand, palms facing forward, press the weights overhead until your arms are fully extended, then lower them back down to shoulder level",
                YoutubeUrl = "https://www.youtube.com/watch?v=4T9UQ4FBVXI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Lateral Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Lie face-down on a bench with a dumbbell in each hand, arms hanging down towards the ground, raise the weights out to the sides until your arms are parallel to the ground, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=rb2bCkoF5Nc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Rear Delt Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Lie face-down on a bench with a dumbbell in each hand, arms hanging down towards the ground, raise the weights out to the sides in an arcing motion, focusing on the rear delts, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=nd_7IJ5d7vA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Seated Arnold Press on Bench",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in each hand, palms facing towards you, press the weights overhead while rotating your palms to face forward at the top of the movement, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=kJt00mS03Q8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Shoulder Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Sit on the edge of a bench with a dumbbell in each hand, shrug your shoulders up towards your ears, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=2I1SI0rInw0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Crunches",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back on a bench with your knees bent and feet flat on the ground, place your hands behind your head, then crunch your upper body towards your knees, contracting your abs",
                YoutubeUrl = "https://www.youtube.com/watch?v=MKmrqcoCZ-M"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Leg Raises on Bench",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back on a bench with your legs extended straight up towards the ceiling, lower your legs down towards the ground, then raise them back up to the starting position, engaging your lower abs",
                YoutubeUrl = "https://www.youtube.com/watch?v=FtDLGdvTShw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Russian Twists",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                Description = "Sit on the edge of a bench with your knees bent and feet flat on the ground, lean back slightly and hold a weight plate or dumbbell with both hands, twist your torso from side to side, engaging your obliques",
                YoutubeUrl = "https://www.youtube.com/watch?v=9Z5fDLn8E7Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench V-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back on a bench with your arms extended overhead, simultaneously raise your legs and upper body towards each other, forming a V-shape, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=UXp0yBofEb8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Leg Pull-ins",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with your hands gripping the edge, extend your legs out straight in front of you, then pull your knees towards your chest, crunching your abs",
                YoutubeUrl = "https://www.youtube.com/watch?v=05RJAdZWT3s"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Step-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand facing a bench with a dumbbell in each hand, step up onto the bench with one foot, then bring the other foot up to meet it, step back down and repeat with the other leg",
                YoutubeUrl = "https://www.youtube.com/watch?v=TGc4VR9bO9k"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Bulgarian Split Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand facing away from a bench with a dumbbell in each hand, place one foot on the bench behind you, lower your body down into a lunge position, then push back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=Qylrx6_-gp8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand in front of a bench with your feet shoulder-width apart, lower your body down into a squat position until your butt touches the bench, then stand back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=As7MwVe4j5k"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Jump Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand in front of a bench with your feet shoulder-width apart, lower your body down into a squat position, then explode upwards into a jump, landing softly back down into the squat position",
                YoutubeUrl = "https://www.youtube.com/watch?v=_l3ySVKYVJ8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Leg Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with your knees bent and feet flat on the ground, extend your legs out straight in front of you, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=TDQiZLMX2iU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Burpees",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand in front of a bench, lower your body down into a squat position, place your hands on the bench, jump your feet back into a plank position, perform a push-up, then jump your feet back in towards your hands and explode upwards into a jump",
                YoutubeUrl = "https://www.youtube.com/watch?v=JZQA08SlJnM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Mountain Climbers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Get into a plank position with your hands on the bench, alternate bringing your knees in towards your chest as if you are climbing a mountain",
                YoutubeUrl = "https://www.youtube.com/watch?v=nmwgirgXLYM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Plank Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Get into a plank position with your hands on dumbbells or kettlebells on the bench, row one weight up towards your hip while stabilizing with the other arm, then switch sides",
                YoutubeUrl = "https://www.youtube.com/watch?v=EDxSP5-EL-s"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Tuck Jumps",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand in front of a bench, lower your body down into a squat position, then explode upwards into a jump, bringing your knees up towards your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=q-NUSwBgNU4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Push-up and Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Get into a push-up position with your hands on dumbbells or kettlebells on the bench, perform a push-up, then row one weight up towards your hip while stabilizing with the other arm, then switch sides",
                YoutubeUrl = "https://www.youtube.com/watch?v=3n3w0adNyH8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Pull-Aparts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Hold a resistance band with both hands in front of you at shoulder-width apart, pull the band apart by bringing your shoulder blades together, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=QS6pHsc5j8Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Bent Over Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Step on a resistance band with both feet, hinge at your hips to bend over, grip the band with both hands, then row the band towards your torso, squeezing your shoulder blades together",
                YoutubeUrl = "https://www.youtube.com/watch?v=X4k73oA-RIM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Lat Pulldowns",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an overhead structure, kneel down or stand with arms extended overhead, pull the band down towards your chest by engaging your lats, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=Fzrz8oIxcug"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Face Pulls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to a fixed point at chest height, grip the band with both hands, palms facing down, pull the band towards your face, keeping your elbows high and out to the sides",
                YoutubeUrl = "https://www.youtube.com/watch?v=4CUqA7mRMoU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Good Mornings",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Step on a resistance band with both feet, position the band across your upper back and shoulders, hinge at your hips to bend forward while keeping your back straight, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=7Ki7FxbIAmc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Bicep Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your palms facing upwards, curl the band up towards your shoulders while keeping your elbows close to your sides, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=op9kVnSso6Q"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Hammer Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your palms facing each other, curl the band up towards your shoulders while keeping your elbows close to your sides, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=Ap8Btf0CkdU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Preacher Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench and place one end of a resistance band under your foot, hold the other end with one hand, brace your arm against your inner thigh and curl the band up towards your shoulder, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=Z5OVoFw0g5U"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Concentration Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench and place one end of a resistance band under your foot, hold the other end with one hand, rest your elbow against your inner thigh and curl the band up towards your shoulder, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=dpHt0ZRvJ8w"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Reverse Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your palms facing downwards, curl the band up towards your shoulders while keeping your elbows close to your sides, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=CrwAkjsR6Lk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band at chest height, hold the ends of the band with your hands, extend your arms forward at chest level, then press the band forward until your arms are fully extended, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=8_eoR1Oe2pA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band at chest height, hold the ends of the band with your hands, extend your arms out to the sides, then bring them together in front of you, squeezing your chest, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=iZJRVbNVxwc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Pullovers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band at chest height, hold the ends of the band with your hands, extend your arms overhead, then pull the band down and back behind your head, stretching your chest, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=nF1BZgIcauE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Squeeze",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band at chest height, hold the ends of the band with your hands, extend your arms forward at chest level, then squeeze your hands together, engaging your chest muscles, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=xUDQcQlqW-U"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Wrap a resistance band around your back and hold the ends in each hand, get into a push-up position with the band providing resistance, lower your body down towards the ground, then push back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=TRnNYvgfgWo"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Tricep Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band overhead, hold one end of the band with both hands, elbows bent and close to your head, extend your arms overhead, straightening them fully, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=Uzri84llCTE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Tricep Pushdowns",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band overhead, hold one end of the band with both hands, elbows bent and close to your sides, extend your arms downward, straightening them fully, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=FZxN59k5pYI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Skull Crushers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band overhead, hold one end of the band with both hands, elbows bent and close to your head, lower the band towards your forehead by bending your elbows, then extend your arms back to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=JcIeP2_uzq0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Tricep Kickbacks",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band at waist height, hold one end of the band with one hand, bend forward at the hips, extend your arm back behind you, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=X-xt4iEr7_g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Close Grip Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Wrap a resistance band around your back and hold the ends in each hand, get into a push-up position with your hands close together, lower your body down towards the ground, then push back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=XaO9eEbMi1o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band at shoulder height, hold one end of the band in each hand, press the band overhead until your arms are fully extended, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=zk_rKmE2_0g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Lateral Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your hands by your sides, raise your arms out to the sides until they are parallel to the ground, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=m-jGG2bKZMU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Front Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your hands in front of your thighs, raise your arms straight out in front of you until they are parallel to the ground, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=y4sjw_ba45Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Face Pulls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Anchor a resistance band at chest height, hold one end of the band in each hand, pull the band towards your face by retracting your shoulder blades, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=4CUqA7mRMoU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Upright Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your hands in front of your thighs, pull the band up towards your chin by raising your elbows out to the sides, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=D8yAwMfcT8k"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your hands by your sides, squat down by bending your knees and pushing your hips back, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=BS7Qa9je3hQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your hands in front of your thighs, hinge at your hips to lower your torso towards the ground while keeping your back straight, then return to the starting position by driving through your heels",
                YoutubeUrl = "https://www.youtube.com/watch?v=cBfNwJI2if8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with one foot, hold the ends of the band with your hands by your sides, step back with your other foot into a lunge position, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=MFoFEZe7Nc8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Glute Bridges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Lie on your back with your knees bent and feet flat on the ground, place a resistance band just above your knees, lift your hips up towards the ceiling, squeezing your glutes at the top, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=czrLz4XoEYc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Side Leg Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                Description = "Attach a resistance band around your ankles, stand with your feet together, then lift one leg out to the side as high as you can, keeping your other leg straight, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=Gxx24w9Ghm0"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Crunches",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your knees bent and feet flat on the ground, place a resistance band around your thighs, cross your arms over your chest or place your hands behind your head, then crunch up towards your knees, squeezing your abs at the top, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=v1J5iNUg6JE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Russian Twists",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the ground with your knees bent and feet elevated, hold the ends of the resistance band with both hands, lean back slightly, then twist your torso from side to side, touching the band to the ground on each side",
                YoutubeUrl = "https://www.youtube.com/watch?v=ffnuEo9_dZk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Bicycle Crunches",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your knees bent and feet elevated, hold the ends of the resistance band with both hands, bring one knee in towards your chest while straightening the other leg out, then twist your torso to bring the opposite elbow towards the bent knee, switch sides in a pedaling motion",
                YoutubeUrl = "https://www.youtube.com/watch?v=3k6quvS0HIA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Plank",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Get into a plank position with the resistance band around your wrists, keeping your body in a straight line from head to heels, engage your core and hold the position for the desired duration",
                YoutubeUrl = "https://www.youtube.com/watch?v=B8IN2cqno9g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Leg Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your legs straight, loop a resistance band around your feet, raise your legs up towards the ceiling, keeping them straight, then lower them back down without letting them touch the ground",
                YoutubeUrl = "https://www.youtube.com/watch?v=AtWUbwBgOeI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Push-up with Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Wrap a resistance band around your back and hold the ends in each hand, get into a push-up position with your hands on the band, perform a push-up, then at the top of the movement, pull one elbow up towards the ceiling, squeezing your shoulder blade, then lower the arm back down and repeat on the other side",
                YoutubeUrl = "https://www.youtube.com/watch?v=3C4JZ8f74fM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Deadlift with Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your hands in front of your thighs, perform a deadlift by hinging at your hips and lowering the band towards the ground, then stand up and press the band overhead, extending your arms fully",
                YoutubeUrl = "https://www.youtube.com/watch?v=UhTVdGNo0wY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Squat with Overhead Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with your hands at shoulder height, perform a squat by bending your knees and pushing your hips back, then stand up explosively while pressing the band overhead",
                YoutubeUrl = "https://www.youtube.com/watch?v=WrRqGk4dA3Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Renegade Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Wrap a resistance band around your back and hold the ends in each hand, get into a push-up position with your hands on the band, perform a push-up, then at the top of the movement, row one elbow up towards the ceiling, squeezing your shoulder blade, then lower the arm back down and repeat on the other side",
                YoutubeUrl = "https://www.youtube.com/watch?v=dCvV1Aobpmo"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Burpees",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band in each hand, squat down and place your hands on the ground, kick your feet back into a push-up position, perform a push-up, then jump your feet back towards your hands and stand up explosively while raising the band overhead",
                YoutubeUrl = "https://www.youtube.com/watch?v=OJf2iPyzmyY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Lie face down on a bench with a dumbbell in each hand, let your arms hang down towards the floor, then row the dumbbells up towards your sides, squeezing your shoulder blades together, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=bEd7G0rP-TQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Pull-overs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                Description = "Lie face up on a bench with a dumbbell in both hands, extend your arms overhead, then lower the dumbbell back behind your head while keeping your arms straight, then pull the weight back up over your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=pq0gf_WL_54"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench T-Bar Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Place one end of a barbell into a landmine attachment or secure it in a corner, straddle the barbell and hold it with both hands, bend your knees and lean forward, then row the barbell towards your chest, squeezing your shoulder blades together, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=BHLO8uU2uGA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Dumbbell Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Sit on the end of a bench with a dumbbell in each hand, let your arms hang down by your sides, then shrug your shoulders up towards your ears, squeezing your traps at the top, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=VkRCBxW9oWM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Face Pulls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                Description = "Sit on a bench and hold a resistance band in each hand, anchor the band under your feet, then pull the bands towards your face, retracting your shoulder blades, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=TxP6UpL6k94"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Dumbbell Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the end of a bench with a dumbbell in each hand, let your arms hang down by your sides, then curl the dumbbells up towards your shoulders, squeezing your biceps at the top, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=i7IwwG8mCAc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Hammer Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the end of a bench with a dumbbell in each hand, palms facing towards each other, let your arms hang down by your sides, then curl the dumbbells up towards your shoulders, keeping your palms facing in, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=8AmZlRXLfRg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Preacher Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench and lean forward, place your upper arms on the bench with your armpits at the edge, hold a barbell with an underhand grip, let your arms hang straight down, then curl the barbell up towards your shoulders, squeezing your biceps at the top, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=yV9KJZrWy6U"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Concentration Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the end of a bench with a dumbbell in one hand, place the back of your upper arm against your inner thigh, let your arm hang straight down, then curl the dumbbell up towards your shoulder, squeezing your biceps at the top, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=U0lMHCB2If8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Cable Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench in front of a cable machine with a straight bar attachment, hold the bar with an underhand grip, elbows by your sides, curl the bar up towards your shoulders, squeezing your biceps at the top, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=KfUaF_XlP9U"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench with your feet flat on the floor, hold a barbell with an overhand grip, lower the barbell to your chest, then press it back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=rT7DgCr-3pg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on an incline bench with your feet flat on the floor, hold a barbell with an overhand grip, lower the barbell to your upper chest, then press it back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=esQi683XR44"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Decline Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a decline bench with your feet secured, hold a barbell with an overhand grip, lower the barbell to your lower chest, then press it back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=U6wfkL8XV6c"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Dumbbell Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench with a dumbbell in each hand, palms facing each other, extend your arms straight up over your chest, then lower the dumbbells out to the sides in an arc motion, keeping a slight bend in your elbows, until your arms are parallel to the ground, then bring them back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=3NQ_Cbz0dVY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Cable Crossovers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Stand in between two cable machines with the pulleys set to the highest position, hold a handle in each hand, palms facing down, take a step forward, then cross your arms in front of you, keeping a slight bend in your elbows, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=0VN8JGyEojk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close Grip Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench with your hands spaced close together on the barbell, lower the barbell to your chest, then press it back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=2PJd0QUJ8j8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Dips",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with your hands on the edge, fingers facing forward, walk your feet out and lower your body down until your elbows are at 90 degrees, then press yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=9vUhhxEaT6w"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Triceps Extensions",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench with a dumbbell in each hand, extend your arms straight up over your chest, palms facing each other, bend your elbows to lower the dumbbells towards your ears, then extend your arms back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=ZlJM5dLNCdo"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Triceps Dips with Feet Elevated",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with your hands on the edge, fingers facing forward, place your feet on another bench in front of you, walk your hands forward and lower your body down until your elbows are at 90 degrees, then press yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=6kALZikXxLc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Skull Crushers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a bench with a dumbbell in each hand, extend your arms straight up over your chest, palms facing each other, bend your elbows to lower the dumbbells towards your forehead, then extend your arms back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=-rh3MHnRI_I"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench with a barbell resting on your upper chest, palms facing forward, press the barbell overhead until your arms are fully extended, then lower it back down to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=CnBmiBqp-AI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Lateral Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the end of a bench with a dumbbell in each hand, palms facing in, let your arms hang down by your sides, then raise the dumbbells out to the sides until they reach shoulder height, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=8hEXS_wqF0c"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Front Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the end of a bench with a dumbbell in each hand, palms facing down, let your arms hang down by your sides, then raise the dumbbells in front of you until they reach shoulder height, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=_opk-VAYCzA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Rear Delt Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Lie face down on a bench with a dumbbell in each hand, palms facing each other, let your arms hang straight down towards the floor, then raise the dumbbells out to the sides until they reach shoulder height, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=KTvxxMS5Gfc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Arnold Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench with a dumbbell in each hand, palms facing towards you, start with the dumbbells at shoulder height with your palms facing towards you, then press the dumbbells overhead while rotating your palms away from you, then lower them back down while rotating your palms back towards you",
                YoutubeUrl = "https://www.youtube.com/watch?v=woayi2Q96wQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Step-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand facing a bench, step one foot up onto the bench, then push through your heel to stand up on top of the bench, then step back down with the same foot and repeat on the other side",
                YoutubeUrl = "https://www.youtube.com/watch?v=6vOhj6pgR7o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Bulgarian Split Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand facing away from a bench with one foot elevated behind you, lower your body down into a lunge position, keeping your front knee behind your toes, then push through your front heel to return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=2C-uNgKwPLE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Burpee Box Jumps",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Start in a standing position in front of a bench, perform a burpee by squatting down, kicking your feet back into a plank position, then jumping your feet back in towards your hands, explosively jump up onto the bench, then step or jump back down and repeat",
                YoutubeUrl = "https://www.youtube.com/watch?v=kJvkiIzndHU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Reverse Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand facing away from a bench, step one foot back onto the bench, then lower your body down into a lunge position, keeping your front knee behind your toes, then push through your front heel to return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=aVWCW7kiy10"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Plank Jacks",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Get into a plank position with your hands on the bench, engage your core and jump both feet out wide to the sides, then jump them back together, continue alternating between jumping out and in",
                YoutubeUrl = "https://www.youtube.com/watch?v=24l_2S_bZJM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Pull-aparts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Hold a resistance band with both hands in front of you, palms facing down and hands shoulder-width apart, pull the band apart and towards your chest, squeezing your shoulder blades together, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=syQ5RYvOuRk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Lat Pulldowns",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an overhead anchor, kneel down and hold the ends of the band with both hands, arms extended overhead, pull the band down towards your chest, squeezing your shoulder blades together, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=syQ5RYvOuRk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Face Pulls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an overhead anchor, hold the ends of the band with both hands, palms facing in, pull the band towards your face, retracting your shoulder blades, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=hyv14e2QDq0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Bent-over Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, hinge at the hips and bend your knees slightly, then row the band towards your lower ribs, squeezing your shoulder blades together, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=2_Fh5XS7H3U"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands in front of your thighs, hinge at the hips and push your hips back, keeping your back flat, then stand up tall, squeezing your glutes at the top, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=Z1wzP0nXD3E"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Bicep Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing up, curl the band up towards your shoulders, squeezing your biceps at the top, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=VPhYipmU2nY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Hammer Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing each other, curl the band up towards your shoulders, keeping your palms facing in, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=rkJrTB_Xjx4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Preacher Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench and place one end of a resistance band under your foot, hold the other end with one hand, rest your elbow on your thigh with your arm extended, curl the band up towards your shoulder, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=bFJx7aWz7TA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Concentration Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on a bench and place one end of a resistance band under your foot, hold the other end with one hand, rest your elbow on your thigh with your arm extended, curl the band up towards your shoulder, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=LjBDB2Kmxv4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Reverse Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing down, curl the band up towards your shoulders, keeping your palms facing down, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=iRtVRbBLufw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an anchor point behind you, hold the ends of the band with both hands, palms facing forward, press the band straight out in front of you until your arms are fully extended, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=7q30zrNCoG0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an anchor point behind you, hold the ends of the band with both hands, palms facing in, extend your arms straight out to the sides, then bring them together in front of you, squeezing your chest at the top, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=KpvDjxGTzOI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Loop a resistance band around your back and hold the ends in each hand, get into a push-up position with your hands on the floor, perform a push-up while maintaining tension on the band, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=Q9JkN8evgSw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Press with Twist",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an anchor point behind you, hold the ends of the band with both hands, palms facing forward, press the band straight out in front of you until your arms are fully extended, then twist your torso to one side, then return to the starting position and repeat on the other side",
                YoutubeUrl = "https://www.youtube.com/watch?v=OK8HsHjpGZg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Chest Pullover",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an anchor point behind you, hold one end of the band with both hands, palms facing up, extend your arms straight out in front of you, then pull the band down towards your thighs, keeping your arms straight, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=OMoGnZagkRo"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing up, press the band straight up overhead until your arms are fully extended, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=XLJ1TnFyf8A"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Lateral Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing in, raise the band out to the sides until your arms are parallel to the ground, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=8hEXS_wqF0c"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Front Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing down, raise the band in front of you until your arms are parallel to the ground, then lower it back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=hVY-57RA84o"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Rear Delt Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an anchor point in front of you, hold the ends of the band with both hands, palms facing each other, pull the band out to the sides until your arms are parallel to the ground, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=pGFB0VziVuw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Shoulder Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing in, shrug your shoulders up towards your ears, then lower them back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=JvC2u6rjdQg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing in, squat down by bending your knees and pushing your hips back, then stand back up, squeezing your glutes at the top",
                YoutubeUrl = "https://www.youtube.com/watch?v=2oSBaXaFZlQ"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Step onto a resistance band with one foot and hold the ends of the band in each hand, step back with the other foot into a lunge position, keeping your front knee behind your toes, then push through your front heel to return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=CS5h5X59xgE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Leg Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an anchor point behind you, hold the ends of the band with both hands, palms facing forward, press your feet against the band as you extend your legs straight out in front of you, then return to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=-hWLQvRYJQs"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Deadlifts",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands in front of your thighs, hinge at the hips and push your hips back, keeping your back flat, then stand up tall, squeezing your glutes at the top",
                YoutubeUrl = "https://www.youtube.com/watch?v=XeruCyC7wP0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Glute Bridges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your knees bent and feet flat on the floor, place a resistance band just above your knees, press your feet into the floor and lift your hips up towards the ceiling, squeezing your glutes at the top, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=dv9R4nJjc8w"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Crunches",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your knees bent and feet flat on the floor, place a resistance band around your mid-back and hold the ends with both hands, cross your arms over your chest, then crunch up towards your knees, squeezing your abs at the top, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=8BBQE9dTxw0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Russian Twists",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the floor with your knees bent and feet elevated, hold the ends of a resistance band with both hands, twist your torso to one side, then twist to the other side, keeping your core engaged throughout",
                YoutubeUrl = "https://www.youtube.com/watch?v=_zR6ROjoOX0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Bicycle Crunches",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your knees bent and feet flat on the floor, place a resistance band around your mid-back and hold the ends with both hands, bring one knee towards your chest while straightening the other leg, simultaneously twist your torso to bring your opposite elbow towards your knee, then switch sides",
                YoutubeUrl = "https://www.youtube.com/watch?v=7UaRkxOT3v0"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Plank",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Get into a plank position with your hands on the floor and a resistance band looped around your wrists, engage your core and hold the position, keeping your body in a straight line from head to heels",
                YoutubeUrl = "https://www.youtube.com/watch?v=zc9jMj6PCEg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Leg Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Abs"),
                SecondaryMuscleGroup = null,
                Description = "Lie on your back with your legs straight and a resistance band looped around your feet, lift your legs towards the ceiling, then lower them back down towards the floor, keeping your core engaged throughout",
                YoutubeUrl = "https://www.youtube.com/watch?v=hQ9UBpQyQrI"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Squat to Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing in, perform a squat by bending your knees and pushing your hips back, then as you stand up, press the band straight up overhead until your arms are fully extended",
                YoutubeUrl = "https://www.youtube.com/watch?v=l0jCh9YYD2A"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Deadlift to Row",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing in, hinge at the hips and push your hips back, keeping your back flat, then stand up tall and pull the band towards your lower ribs, squeezing your shoulder blades together",
                YoutubeUrl = "https://www.youtube.com/watch?v=kZ-u7My4zvY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Woodchoppers",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Attach a resistance band to an anchor point above your head, hold the ends of the band with both hands, stand with your feet shoulder-width apart and arms extended overhead, then pull the band diagonally across your body, pivoting on your back foot and twisting your torso",
                YoutubeUrl = "https://www.youtube.com/watch?v=RVWZ1yC7kVk"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Standing Oblique Crunches",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Stand on a resistance band with both feet, hold the ends of the band with both hands, palms facing in, extend your arms overhead, then crunch your torso to one side, bringing your elbow towards your hip, then return to the starting position and repeat on the other side",
                YoutubeUrl = "https://www.youtube.com/watch?v=DRhW-EQCHjE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Band Bear Crawls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Band"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Full-Body"),
                SecondaryMuscleGroup = null,
                Description = "Place a resistance band around your wrists and get into a bear crawl position with your knees hovering above the ground, walk forward by moving your opposite hand and foot at the same time, keeping tension on the band",
                YoutubeUrl = "https://www.youtube.com/watch?v=pI-5R4-mo-g"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Pull-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a pull-up bar with your hands slightly wider than shoulder-width apart, pull yourself up until your chin clears the bar, then lower yourself back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=eGo4IYlbE5g"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Chin-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a pull-up bar with your palms facing towards you and hands shoulder-width apart, pull yourself up until your chin clears the bar, then lower yourself back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=zJMQHBGgO8I"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Inverted Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Set up a bar at hip height, lie underneath it and grab it with an overhand grip, hang from the bar with your body straight and heels on the ground, pull your chest up to the bar, then lower yourself back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=ebWEh1OAy0s"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bodyweight Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Set up a bar at waist height, grab it with an overhand grip, lean back with your body straight and heels on the ground, pull your chest up to the bar, then lower yourself back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=cA5DzXtxtkw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Superman",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Lie face down on the ground with your arms extended overhead and legs straight, lift your chest, arms, and legs off the ground simultaneously, squeezing your back muscles at the top, then lower back down",
                YoutubeUrl = "https://www.youtube.com/watch?v=cc8of2IO4x0"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Chin-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a pull-up bar with your palms facing towards you and hands shoulder-width apart, pull yourself up until your chin clears the bar, then lower yourself back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=zJMQHBGgO8I"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close-grip Pull-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Hang from a pull-up bar with your hands closer together than shoulder-width apart, palms facing towards you, pull yourself up until your chin clears the bar, then lower yourself back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=4eSOvVBHr-U"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bodyweight Bicep Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet shoulder-width apart and arms extended by your sides, palms facing up, curl your hands towards your shoulders, squeezing your biceps at the top, then lower them back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=6kALZikXxLc"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bicep Dips",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Set up two parallel bars at hip height, grip the bars with your palms facing towards each other, lower yourself down by bending your elbows, then push yourself back up to the starting position, keeping tension on your biceps",
                YoutubeUrl = "https://www.youtube.com/watch?v=8L7KJjdLVw8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Negative Chin-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Jump up to the top of a pull-up position so your chin is over the bar, then lower yourself down as slowly as possible, focusing on the negative portion of the movement",
                YoutubeUrl = "https://www.youtube.com/watch?v=gqVCIcGrZrc"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with your hands shoulder-width apart, lower your body until your chest almost touches the ground, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=IODxDxX7oi4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Wide-grip Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with your hands wider than shoulder-width apart, lower your body until your chest almost touches the ground, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=_83TzKk_2Mg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Diamond Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with your hands close together, forming a diamond shape with your index fingers and thumbs, lower your body until your chest almost touches your hands, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=J9b8NmIc0z8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Decline Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Place your feet on an elevated surface, such as a bench or step, and assume a plank position with your hands on the ground, lower your body until your chest almost touches the ground, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=TU8QYVW0gDU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Plyometric Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Perform a regular push-up, but explosively push yourself up so your hands leave the ground, clap your hands together, then land softly and immediately go into the next repetition",
                YoutubeUrl = "https://www.youtube.com/watch?v=45cFHORjWJ0"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Tricep Dips",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Set up two parallel bars at hip height, grip the bars with your palms facing towards each other, lower yourself down by bending your elbows, then push yourself back up to the starting position, keeping tension on your triceps",
                YoutubeUrl = "https://www.youtube.com/watch?v=8L7KJjdLVw8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Diamond Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with your hands close together, forming a diamond shape with your index fingers and thumbs, lower your body until your chest almost touches your hands, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=J9b8NmIc0z8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Tricep Bench Dips",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with your hands gripping the edge beside your hips, slide your butt off the bench and lower yourself down by bending your elbows, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=_2wzczWYgTI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Tricep Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with your hands close together and elbows tight to your sides, lower your body until your chest almost touches the ground, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=yKWYEw_k06I"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Close-grip Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Triceps"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with your hands closer together than shoulder-width apart, lower your body until your chest almost touches the ground, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=4eSOvVBHr-U"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Handstand Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Start in a handstand position against a wall, lower your body until your head almost touches the ground, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=ABbVqVubrDE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Pike Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with your hips raised towards the ceiling, lower your head towards the ground by bending your elbows, then push yourself back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=gZHRniTAKpY"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bodyweight Shoulder Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet shoulder-width apart and arms bent, press your hands upwards until your arms are fully extended overhead, then lower them back down to shoulder level",
                YoutubeUrl = "https://www.youtube.com/watch?v=-rh3MHnRI_I"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dive Bomber Push-ups",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Start in a downward dog position with your hips raised towards the ceiling and your hands and feet on the ground, lower your chest towards the ground by bending your elbows, then arch your back and push your chest upwards as you straighten your arms",
                YoutubeUrl = "https://www.youtube.com/watch?v=M6vpFV6Wkl4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Wall Walks",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Shoulders"),
                SecondaryMuscleGroup = null,
                Description = "Start in a plank position with your feet against a wall, walk your hands towards the wall as you walk your feet up the wall, keep walking until you are in a handstand position against the wall, then walk back down to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=s_GWBixx_eE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bodyweight Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet shoulder-width apart, squat down by bending your knees and pushing your hips back, then stand back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=QFvNhsWMU0c"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Lunges",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet together, step forward with one foot and lower your body until both knees are bent at a 90-degree angle, then push back up to the starting position and repeat on the other side",
                YoutubeUrl = "https://www.youtube.com/watch?v=B3u9UelYlpU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Jump Squats",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet shoulder-width apart, squat down by bending your knees and pushing your hips back, then explode upwards into a jump, land softly and immediately go into the next repetition",
                YoutubeUrl = "https://www.youtube.com/watch?v=JB2oyawG9KI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Calf Raises",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Stand with your feet shoulder-width apart, raise your heels off the ground by pushing through the balls of your feet, then lower them back down to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=2PdJFbjWHEU"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Burpees",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bodyweight"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Legs"),
                SecondaryMuscleGroup = null,
                Description = "Start in a standing position, squat down and place your hands on the ground, jump your feet back into a plank position, do a push-up, jump your feet back towards your hands, then explode upwards into a jump",
                YoutubeUrl = "https://www.youtube.com/watch?v=JZQA08SlJnM"
            });


            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Lie face down on a bench with a dumbbell in each hand, let your arms hang straight down towards the floor, pull the dumbbells up towards your chest by squeezing your shoulder blades together, then lower them back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=D1tdA_e3jKw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Pull-overs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Lie face up on a bench with a dumbbell in both hands, lower the dumbbell back over your head until your arms are parallel to the ground, then pull the dumbbell back up over your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=7OoII_aT7OE"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench T-bar Rows",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Place one end of a barbell in the corner of a bench and straddle the bench, hold the other end of the barbell with one hand, pull the barbell up towards your chest by squeezing your shoulder blade, then lower it back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=o7wZPW6Y2hw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Shrugs",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in each hand, let your arms hang straight down towards the floor, shrug your shoulders up towards your ears, then lower them back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=OujIzKssvJg"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Reverse Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Back"),
                SecondaryMuscleGroup = null,
                Description = "Lie face down on a bench with a dumbbell in each hand, let your arms hang straight down towards the floor, lift the dumbbells out to the side until your arms are parallel to the ground, then lower them back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=k5GjKyvT1rw"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Bicep Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in each hand, let your arms hang straight down towards the floor, curl the dumbbells towards your shoulders, then lower them back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=iu8B3XaF2Ow"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Hammer Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in each hand, let your arms hang straight down towards the floor, curl the dumbbells towards your shoulders with a neutral grip, then lower them back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=SPbSdfpH3SM"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Preacher Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in each hand, rest your upper arms against the bench with your elbows bent, curl the dumbbells towards your shoulders, then lower them back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=z_GIyd9rNJI"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Concentration Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on the edge of a bench with a dumbbell in one hand, rest your elbow against your inner thigh, curl the dumbbell towards your shoulder, then lower it back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=Woq_GNcsQz8"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Incline Curls",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Biceps"),
                SecondaryMuscleGroup = null,
                Description = "Sit on an incline bench with a dumbbell in each hand, let your arms hang straight down towards the floor, curl the dumbbells towards your shoulders, then lower them back down with control",
                YoutubeUrl = "https://www.youtube.com/watch?v=FbI_8OQuE1s"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie flat on a bench with a barbell above your chest, lower the barbell to your chest, then push it back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=gRVjAtPip0Y"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Incline Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on an incline bench with a barbell above your chest, lower the barbell to your upper chest, then push it back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=8igDA_9b1vA"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Dumbbell Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie flat on a bench with a dumbbell in each hand, lower the dumbbells to your chest, then push them back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=8D1cRy3Oz4s"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Decline Bench Press",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie on a decline bench with a barbell above your chest, lower the barbell to your lower chest, then push it back up to the starting position",
                YoutubeUrl = "https://www.youtube.com/watch?v=v5b-YGG6Gn4"
            });

            _context.Exercises.Add(new Exercise()
            {
                Name = "Chest Flyes",
                Priority = 1,
                Equipment = _context.Equipment.Single(e => e.Name == "Bench"),
                PrimaryMuscleGroup = _context.MuscleGroups.Single(e => e.Name == "Chest"),
                SecondaryMuscleGroup = null,
                Description = "Lie flat on a bench with a dumbbell in each hand, arms extended above your chest, lower the dumbbells out to the sides in a wide arc, then bring them back together above your chest",
                YoutubeUrl = "https://www.youtube.com/watch?v=kaWvxPb7yX4"
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


