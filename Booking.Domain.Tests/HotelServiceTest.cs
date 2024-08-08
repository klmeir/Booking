using System.Linq.Expressions;

namespace Booking.Domain.Tests;

using Moq;
using Xunit;
using Entities;
using Ports;
using Services;
using Exception;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

public class HotelServiceTests
{
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly HotelService _hotelService;

    public HotelServiceTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _roomRepositoryMock = new Mock<IRoomRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _hotelService = new HotelService(
            _hotelRepositoryMock.Object,
            _roomRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task SaveHotelAsync_ShouldSaveHotel()
    {
        // Arrange
        var hotel = new Hotel { Id = 1, Name = "Test Hotel" };
        _hotelRepositoryMock.Setup(repo => repo.SaveHotel(It.IsAny<Hotel>()))
            .ReturnsAsync(hotel);
        
        // Act
        var result = await _hotelService.SaveHotelAsync(hotel);

        // Assert
        _hotelRepositoryMock.Verify(repo => repo.SaveHotel(It.IsAny<Hotel>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(hotel, result);
    }

    [Fact]
    public async Task SingleHotelAsync_ShouldReturnHotel()
    {
        // Arrange
        var hotel = new Hotel { Id = 1, Name = "Test Hotel" };
        _hotelRepositoryMock.Setup(repo => repo.SingleHotel(1))
            .ReturnsAsync(hotel);

        // Act
        var result = await _hotelService.SingleHotelAsync(1);

        // Assert
        _hotelRepositoryMock.Verify(repo => repo.SingleHotel(1), Times.Once);
        Assert.Equal(hotel, result);
    }

    [Fact]
    public async Task SingleHotelAsync_ShouldThrowNotFoundException()
    {
        // Arrange
        _hotelRepositoryMock.Setup(repo => repo.SingleHotel(It.IsAny<int>()))
            .ReturnsAsync((Hotel)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _hotelService.SingleHotelAsync(1));
    }

    [Fact]
    public async Task CheckIfExistsHotelAsync_ShouldNotThrowException()
    {
        // Arrange
        var hotel = new Hotel { Id = 1, Name = "Test Hotel" };
        _hotelRepositoryMock.Setup(repo => repo.SingleHotel(1))
            .ReturnsAsync(hotel);

        // Act & Assert
        await _hotelService.CheckIfExistsHotelAsync(1);
    }

    [Fact]
    public async Task CheckIfExistsHotelAsync_ShouldThrowCoreBusinessException()
    {
        // Arrange
        _hotelRepositoryMock.Setup(repo => repo.SingleHotel(It.IsAny<int>()))
            .ReturnsAsync((Hotel)null);

        // Act & Assert
        await Assert.ThrowsAsync<CoreBusinessException>(() => _hotelService.CheckIfExistsHotelAsync(1));
    }

    [Fact]
    public async Task UpdateHotelAsync_ShouldUpdateHotel()
    {
        // Arrange
        var hotel = new Hotel { Id = 1, Name = "Updated Hotel" };
        _hotelRepositoryMock.Setup(repo => repo.UpdateHotel(It.IsAny<Hotel>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _hotelService.UpdateHotelAsync(hotel);

        // Assert
        _hotelRepositoryMock.Verify(repo => repo.UpdateHotel(It.IsAny<Hotel>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(hotel, result);
    }

    [Fact]
    public async Task HotelAvailabilityAsync_ShouldReturnAvailableHotels()
    {
        // Arrange
        var searchAvailability = new SearchAvailability("Test City", 2, "2024-08-10", "2024-08-15");
        var hotel = new Hotel { Id = 1, Name = "Available Hotel", City = "Test City", IsActive = true };
        var room = new Room
        {
            Hotel = hotel,
            IsActive = true,
            MaxGuests = 2,
            Reservations = new List<Reservation>()
        };
        _roomRepositoryMock.Setup(repo => repo.GetManyAsync(
            It.IsAny<Expression<Func<Room, bool>>>(), 
            It.IsAny<Func<IQueryable<Room>, IOrderedQueryable<Room>>>(), 
            It.IsAny<string>()
        )).ReturnsAsync(new List<Room> { room });

        // Act
        var result = await _hotelService.HotelAvailabilityAsync(searchAvailability);

        // Assert
        Assert.Single(result);
        Assert.Contains(hotel, result);
    }
}
