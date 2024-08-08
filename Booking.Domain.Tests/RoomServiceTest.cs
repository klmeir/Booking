namespace Booking.Domain.Tests;

using Moq;
using Xunit;
using Booking.Domain.Entities;
using Booking.Domain.Exception;
using Booking.Domain.Ports;
using Booking.Domain.Services;
using System.Threading.Tasks;
using System.Threading;

public class RoomServiceTests
{
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly RoomService _roomService;

    public RoomServiceTests()
    {
        _roomRepositoryMock = new Mock<IRoomRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _hotelRepositoryMock = new Mock<IHotelRepository>();
            
        var hotelService = new HotelService(
            _hotelRepositoryMock.Object,
            _roomRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
        _roomService = new RoomService(
            _roomRepositoryMock.Object,
            _unitOfWorkMock.Object,
            hotelService
        );
    }

    [Fact]
    public async Task SaveRoomAsync_ShouldSaveRoom()
    {
        // Arrange
        var room = new Room { Id = 1, HotelId = 1, Location = "Test Room" };
        var hotel = new Hotel { Id = 1, Name = "Test Hotel" };
        _hotelRepositoryMock.Setup(repo => repo.SingleHotel(1))
            .ReturnsAsync(hotel);
        _roomRepositoryMock.Setup(repo => repo.SaveRoom(It.IsAny<Room>()))
            .ReturnsAsync(room);

        // Act
        var result = await _roomService.SaveRoomAsync(room);

        // Assert
        _hotelRepositoryMock.Verify(service => service.SingleHotel(room.HotelId), Times.Once);
        _roomRepositoryMock.Verify(repo => repo.SaveRoom(It.IsAny<Room>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(room, result);
    }

    [Fact]
    public async Task SingleRoomAsync_ShouldReturnRoom()
    {
        // Arrange
        var room = new Room { Id = 1, HotelId = 1, Location = "Test Room" };
        _roomRepositoryMock.Setup(repo => repo.SingleRoom(1))
            .ReturnsAsync(room);

        // Act
        var result = await _roomService.SingleRoomAsync(1);

        // Assert
        _roomRepositoryMock.Verify(repo => repo.SingleRoom(1), Times.Once);
        Assert.Equal(room, result);
    }

    [Fact]
    public async Task SingleRoomAsync_ShouldThrowNotFoundException()
    {
        // Arrange
        _roomRepositoryMock.Setup(repo => repo.SingleRoom(It.IsAny<int>()))
            .ReturnsAsync((Room)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _roomService.SingleRoomAsync(1));
    }

    [Fact]
    public async Task UpdateRoomAsync_ShouldUpdateRoom()
    {
        // Arrange
        var room = new Room { Id = 1, HotelId = 1, Location = "Updated Room" };
        _roomRepositoryMock.Setup(repo => repo.UpdateRoom(It.IsAny<Room>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _roomService.UpdateRoomAsync(room);

        // Assert
        _roomRepositoryMock.Verify(repo => repo.UpdateRoom(It.IsAny<Room>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(room, result);
    }

    [Fact]
    public async Task CheckIfExistsRoomAsync_ShouldNotThrowException()
    {
        // Arrange
        var room = new Room { Id = 1, HotelId = 1, Location = "Test Room" };
        _roomRepositoryMock.Setup(repo => repo.SingleRoom(1))
            .ReturnsAsync(room);

        // Act & Assert
        await _roomService.CheckIfExistsRoomAsync(1);
    }

    [Fact]
    public async Task CheckIfExistsRoomAsync_ShouldThrowCoreBusinessException()
    {
        // Arrange
        _roomRepositoryMock.Setup(repo => repo.SingleRoom(It.IsAny<int>()))
            .ReturnsAsync((Room)null);

        // Act & Assert
        await Assert.ThrowsAsync<CoreBusinessException>(() => _roomService.CheckIfExistsRoomAsync(1));
    }
}
