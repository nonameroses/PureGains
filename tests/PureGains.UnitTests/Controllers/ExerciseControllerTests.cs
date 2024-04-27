using Api.Controllers;
using Application.Features.Exercises.Dtos;
using Application.Features.Exercises.Queries;
using Domain.Entities;
using MediatR;
using Moq;

namespace PureGains.UnitTests.Controllers;
public class ExerciseControllerTests
{
    [Fact]
    public void Constructor_Without_Mediator_ThrowsNullExcepton()
    {
        IMediator nullMediator = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ExercisesController(nullMediator));
    }

    [Fact]
    public async Task GetExercises_ReturnsExpectedExercises()
    {
        // Arriange
        var mockMediator = new Mock<IMediator>();
        var expectedResult = new List<Exercise>
        {
            new Exercise { Id = 1, Name = "Kettlebell High Pull", Description = "Pull the kettlebell up to your shoulder, leading with your elbow", Priority = 5, YoutubeUrl = "https://www.youtube.com/watch?v=0B5JwVgFLP4", EquipmentId = 1, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = null },
            new Exercise { Id = 2, Name = "Kettlebell Renegade Row", Description = "In a push-up position with hands on two kettlebells, row one kettlebell up while stabilizing your body with the other arm.", Priority = 5, YoutubeUrl = "https://www.youtube.com/watch?v=Zp26q4BY5HE", EquipmentId = 1, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = 6 },
            new Exercise { Id = 3, Name = "Dumbbell Row", Description = "Bend over a bench and row the dumbbell back towards your hip.", Priority = 1, YoutubeUrl = "https://www.youtube.com/watch?v=pYcpY20QaE8", EquipmentId = 2, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = 2 },
            new Exercise { Id = 4, Name = "Dumbbell Deadlift", Description = "Stand with feet hip-width apart, squat down to pick up the dumbbells, and stand up.", Priority = 2, YoutubeUrl = "https://www.youtube.com/watch?v=ytGaGIn3SjE", EquipmentId = 2, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = 7 },
            new Exercise { Id = 5, Name = "Dumbbell Pullover", Description = "Lie on a bench holding a dumbbell with both hands above your chest, then lower it behind your head and bring it back up.", Priority = 3, YoutubeUrl = "https://www.youtube.com/watch?v=0G2_XV7slIg", EquipmentId = 2, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = 3 },
            new Exercise { Id = 6, Name = "Single Arm Dumbbell Deadlift", Description = "Perform a deadlift while holding a dumbbell in one hand, switching hands after a set.", Priority = 4, YoutubeUrl = "https://www.youtube.com/watch?v=sq4VAZ1TtRw", EquipmentId = 2, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = 7 },
            new Exercise { Id = 7, Name = "Bent Over Two-Dumbbell Row", Description = "With a dumbbell in each hand, bend over at about a 45-degree angle and row the weights back towards your hips.", Priority = 5, YoutubeUrl = "https://www.youtube.com/watch?v=L2fvpxrfJfQ", EquipmentId = 2, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = 2 },
            new Exercise { Id = 8, Name = "Barbell Row", Description = "Bend over and pull the barbell towards your waist, keeping your back straight.", Priority = 1, YoutubeUrl = "https://www.youtube.com/watch?v=G8l_8chR5BE", EquipmentId = 3, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = 2 },
            new Exercise { Id = 9, Name = "Deadlift", Description = "Stand with your mid-foot under the barbell, bend over and grab it, then stand up with the weight.", Priority = 2, YoutubeUrl = "https://www.youtube.com/watch?v=3UwO0fKukRw", EquipmentId = 3, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = 7 },
            new Exercise { Id = 10, Name = "T-Bar Row", Description = "Load one end of a barbell, straddle it, and row the bar towards your chest.", Priority = 3, YoutubeUrl = "https://www.youtube.com/watch?v=j3Igk5nyZE4", EquipmentId = 3, PrimaryMuscleGroupId = 1, SecondaryMuscleGroupId = 2 }

        };

        mockMediator.Setup(m => m.Send(It.IsAny<GetExercises.Query>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new ExercisesController(mockMediator.Object);

        var requestDto = new ExerciseRequestDto
        {
            EquipmentIds = new List<int> { 1, 2, 3 },
            MuscleGroupIds = new List<int> { 1 }
        };


        //Act

        var result = await controller.GetInitialExercisesForUser(requestDto);

        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
        mockMediator.Verify(m => m.Send(It.IsAny<GetExercises.Query>(), It.IsAny<CancellationToken>()), Times.Once);

    }

}
