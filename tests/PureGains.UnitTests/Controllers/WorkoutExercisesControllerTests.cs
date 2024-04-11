using Api.Controllers;
using Application.Features.WorkoutExercises.Queries;
using Domain.Entities;
using MediatR;
using Moq;

namespace PureGains.UnitTests.Controllers;
public class WorkoutExercisesControllerTests
{
    [Fact]
    public void Constructor_Without_Mediator_ThrowsNullExcepton()
    {
        IMediator nullMediator = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new WorkoutExercisesController(nullMediator));
    }
    [Fact]
    public async Task GetEquipment_ReturnsExpectedEquipment()
    {
        // Arriange
        var mockMediator = new Mock<IMediator>();
        var expectedResult = new List<WorkoutExercise>
        {
            new WorkoutExercise { Id = 1, WorkoutId = 1, ExerciseId =5 , Sets = 3, Reps = 6 },
            new WorkoutExercise { Id = 1, WorkoutId = 1, ExerciseId =2 , Sets = 3, Reps = 10 },
            new WorkoutExercise { Id = 1, WorkoutId = 1, ExerciseId =12 , Sets = 3, Reps = 12 },
        };

        mockMediator.Setup(m => m.Send(It.IsAny<GetWorkoutExercises.Query>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new WorkoutExercisesController(mockMediator.Object);

        //Act
        var result = await controller.GetWorkoutExercises();

        Assert.Equal(expectedResult, result);

    }
}
