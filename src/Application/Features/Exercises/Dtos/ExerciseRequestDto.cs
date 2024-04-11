namespace Application.Features.Exercises.Dtos;
public class ExerciseRequestDto
{
    public List<int> EquipmentIds { get; set; } = [];
    public List<int> MuscleGroupIds { get; set; } = [];
}
