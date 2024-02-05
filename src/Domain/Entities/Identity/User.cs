namespace Domain.Entities.Identity;

public class User
{
    public int Id { get; set; }
    public string? Auth0UserId { get; set; }
    public string? Email { get; set; }
    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
    public string? Nickname { get; set; }
    public DateTime CreatedAt { get; set; }

    public bool IsProfileCreated { get; set; } = false;
    //public DateTime UpdatedAt { get; set; }
}