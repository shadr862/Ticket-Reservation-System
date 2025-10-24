using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Ticket_Reservation_System_API.Dtos;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Model;
using Ticket_Reservation_System_API.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace Ticket_Reservation_System_API.Tests
{
    public class BookingServiceTests
    {
        [Fact]
        public async Task BookSeatAsync_ShouldReturnSuccess_WhenSeatsAreAvailable()
        {
            // Arrange
            var busScheduleId = Guid.NewGuid();
            var seat1 = new Seat { Id = Guid.NewGuid(), Status = SeatStatus.Available };
            var seat2 = new Seat { Id = Guid.NewGuid(), Status = SeatStatus.Available };
            var schedule = new BusSchedule
            {
                Id = busScheduleId,
                Price = 700,
                JourneyDate = new DateTime(2025, 11, 15),
                StartTime = new TimeSpan(8, 0, 0),
                ArrivalTime = new TimeSpan(14, 0, 0),
                Bus = new Bus { Id = Guid.NewGuid(), BusName = "Green Express", CompanyName = "Green Line" },
                Route = new Route { Id = Guid.NewGuid(), From = "Dhaka", To = "Chittagong" },
                Seats = new List<Seat>()
            };


            var mockSeatRepo = new Mock<ISeatRepository>();
            var mockScheduleRepo = new Mock<IBusScheduleRepository>();
            var mockTicketRepo = new Mock<ITicketRepository>();
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<IDbContextTransaction>();

            mockScheduleRepo.Setup(r => r.GetByIdAsync(busScheduleId)).ReturnsAsync(schedule);
            mockSeatRepo.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                        .ReturnsAsync(new List<Seat> { seat1, seat2 });

            mockUow.Setup(u => u.BeginTransactionAsync())
                   .Returns(Task.FromResult(mockTransaction.Object)); // FIXED HERE
            mockUow.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var service = new BookingService(
                mockSeatRepo.Object, mockScheduleRepo.Object, mockTicketRepo.Object,
                mockPassengerRepo.Object, mockUow.Object);

            var input = new BookSeatInputDto
            {
                BusScheduleId = busScheduleId,
                SeatIds = new List<Guid> { seat1.Id, seat2.Id },
                PassengerName = "John Doe",
                PassengerMobile = "0123456789",
                BoardingPoint = "Dhaka",
                DroppingPoint = "Chittagong"
            };

            // Act
            var result = await service.BookSeatAsync(input);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Booked successfully.");

            mockSeatRepo.Verify(r => r.UpdateAsync(It.IsAny<Seat>()), Times.Exactly(2));
            mockPassengerRepo.Verify(r => r.AddAsync(It.IsAny<Passenger>()), Times.Once);
            mockTicketRepo.Verify(r => r.AddAsync(It.IsAny<Ticket>()), Times.Once);
            mockUow.Verify(u => u.SaveChangesAsync(), Times.Once);
            mockTransaction.Verify(t => t.CommitAsync(default), Times.Once);
        }

        [Fact]
        public async Task BookSeatAsync_ShouldFail_WhenSeatAlreadyBooked()
        {
            // Arrange
            var busScheduleId = Guid.NewGuid();
            var seat = new Seat { Id = Guid.NewGuid(), Status = SeatStatus.Booked };
            var schedule = new BusSchedule { Id = busScheduleId, Price = 700 };

            var mockSeatRepo = new Mock<ISeatRepository>();
            var mockScheduleRepo = new Mock<IBusScheduleRepository>();
            var mockTicketRepo = new Mock<ITicketRepository>();
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockScheduleRepo.Setup(r => r.GetByIdAsync(busScheduleId)).ReturnsAsync(schedule);
            mockSeatRepo.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                        .ReturnsAsync(new List<Seat> { seat });

            var service = new BookingService(
                mockSeatRepo.Object, mockScheduleRepo.Object, mockTicketRepo.Object,
                mockPassengerRepo.Object, mockUow.Object);

            var input = new BookSeatInputDto
            {
                BusScheduleId = busScheduleId,
                SeatIds = new List<Guid> { seat.Id },
                PassengerName = "Jane Doe",
                PassengerMobile = "01987654321",
                BoardingPoint = "Dhaka",
                DroppingPoint = "Chittagong"
            };

            // Act
            var result = await service.BookSeatAsync(input);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("One or more seats already booked.");

            mockSeatRepo.Verify(r => r.UpdateAsync(It.IsAny<Seat>()), Times.Never);
            mockTicketRepo.Verify(r => r.AddAsync(It.IsAny<Ticket>()), Times.Never);
        }
    }
}
