namespace Application.Features.Exercises.Dtos;
public class ExerciseRequestDto
{
    public List<int> EquipmentIds { get; set; } = new List<int>();
    public List<int> MuscleGroupIds { get; set; } = new List<int>();
}
