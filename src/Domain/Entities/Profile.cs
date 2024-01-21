namespace Domain.Entities;
public class Profile
{
    public int Id { get; set; }
    //public DateTime Dob { get; set; }
    public string? Gender { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int UserId { get; set; }
}