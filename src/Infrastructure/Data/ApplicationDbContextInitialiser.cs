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
            _context.Equipment.Add(new Equipment { Id = 1, Name = "Kettlebell" });
            _context.Equipment.Add(new Equipment { Id = 2, Name = "Dumbbell" });
            _context.Equipment.Add(new Equipment { Id = 3, Name = "Barbell" });
            _context.Equipment.Add(new Equipment { Id = 4, Name = "Plate" });
            _context.Equipment.Add(new Equipment { Id = 5, Name = "Pull-up Bar" });
            _context.Equipment.Add(new Equipment { Id = 6, Name = "Bench" });
            _context.Equipment.Add(new Equipment { Id = 7, Name = "Band" });
            _context.Equipment.Add(new Equipment { Id = 8, Name = "Bodyweight" });

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
    }
}
