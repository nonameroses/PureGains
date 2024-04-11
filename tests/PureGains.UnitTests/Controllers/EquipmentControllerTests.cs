using Api.Controllers;
using Application.Features.Equipment.Queries;
using Domain.Entities;
using MediatR;
using Moq;

namespace PureGains.UnitTests.Controllers;

public class EquipmentControllerTests
{
    [Fact]
    public void Constructor_Without_Mediator_ThrowsNullExcepton()
    {
        IMediator nullMediator = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new EquipmentController(nullMediator));
    }
    [Fact]
    public async Task GetEquipment_ReturnsExpectedEquipment()
    {
        // Arriange
        var mockMediator = new Mock<IMediator>();
        var expectedResult = new List<Equipment>
        {
            new Equipment { Id = 1, ImagePath = "../assets/equipment/kettlebell-black.png", Name = "Kettlebell" },
            new Equipment { Id = 2, ImagePath = "../assets/equipment/dumbbell-black.png", Name = "Dumbbell" },
            new Equipment { Id = 3, ImagePath = "../assets/equipment/barbell-black.png", Name = "Barbell" },
            new Equipment { Id = 4, ImagePath = "../assets/equipment/weight-plates-black.png", Name = "Plate" },
            new Equipment { Id = 5, ImagePath = "../assets/equipment/pull-up-bar.png", Name = "Pull-up Bar" },
            new Equipment { Id = 6, ImagePath = "../assets/equipment/bench-press.png", Name = "Bench" },
            new Equipment { Id = 7, ImagePath = "../assets/equipment/resistance-band.png", Name = "Band" },
            new Equipment { Id = 8, ImagePath = "../assets/equipment/bodyweight.png", Name = "Bodyweight" },
        };

        mockMediator.Setup(m => m.Send(It.IsAny<GetEquipment.Query>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new EquipmentController(mockMediator.Object);

        //Act
        var result = await controller.GetEquipment();

        Assert.Equal(expectedResult, result);

    }
}
