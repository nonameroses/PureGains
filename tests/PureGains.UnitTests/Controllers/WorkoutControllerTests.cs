using Api.Controllers;
using Application.Features.Workout.Queries;
using Domain.Entities;
using MediatR;
using Moq;

namespace PureGains.UnitTests.Controllers;
public class WorkoutControllerTests
{
    [Fact]
    public void Constructor_Without_Mediator_ThrowsNullExcepton()
    {
        IMediator nullMediator = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new WorkoutController(nullMediator));
    }
    [Fact]
    public async Task GetEquipment_ReturnsExpectedEquipment()
    {
        // Arriange
        var mockMediator = new Mock<IMediator>();

        var workoutExercises = new List<WorkoutExercise>
        {
            new WorkoutExercise { Id = 1, WorkoutId = 1, ExerciseId =5 , Sets = 3, Reps = 6 },
            new WorkoutExercise { Id = 1, WorkoutId = 1, ExerciseId =2 , Sets = 3, Reps = 10 },
            new WorkoutExercise { Id = 1, WorkoutId = 1, ExerciseId =12 , Sets = 3, Reps = 12 },
        };
        var expectedResult = new List<Workout>
        {
            new Workout
            {
                Id = 1,
                Date = DateTime.Now,
                UserId = 1,
                WorkoutExercises = workoutExercises
            }
        };

        mockMediator.Setup(m => m.Send(It.IsAny<GetWorkoutsForUser.Query>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new WorkoutController(mockMediator.Object);

        //Act
        var result = await controller.GetWorkouts();

        Assert.Equal(expectedResult, result);

    }
}
