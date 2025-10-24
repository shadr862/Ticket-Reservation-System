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
using Xunit;
using xunitexample.tests.MockData;

namespace Ticket_Reservation_System_API.Tests
{
    public class SearchServiceTests
    {
        [Fact]
        public async Task SearchAvailableBusesAsync_ShouldReturnMappedBusList_WhenSchedulesExist()
        {
            // Arrange
            var journeyDate = new DateTime(2025, 11, 15);
            var mockRepo = new Mock<IBusScheduleRepository>();

            var schedules = SearchMockData.result();

            mockRepo.Setup(r => r.GetSchedulesByRouteAndDateAsync("Dhaka", "Chittagong", journeyDate))
                    .ReturnsAsync(schedules);

            var service = new SearchService(mockRepo.Object);

            // Act
            var result = await service.SearchAvailableBusesAsync("Dhaka", "Chittagong", journeyDate);

            // Assert
            result.Should().NotBeNull().And.HaveCount(1);

            var bus = result.First();
            bus.CompanyName.Should().Be("Green Line");
            bus.BusName.Should().Be("Green Express");
            bus.From.Should().Be("Dhaka");
            bus.To.Should().Be("Chittagong");
            bus.Price.Should().Be(1500);
            bus.SeatsLeft.Should().Be(2);

            mockRepo.Verify(r => r.GetSchedulesByRouteAndDateAsync("Dhaka", "Chittagong", journeyDate), Times.Once);
        }

        [Fact]
        public async Task SearchAvailableBusesAsync_ShouldReturnEmptyList_WhenNoSchedulesFound()
        {
            // Arrange
            var mockRepo = new Mock<IBusScheduleRepository>();
            var date = new DateTime(2025, 11, 15);

            mockRepo.Setup(r => r.GetSchedulesByRouteAndDateAsync("Dhaka", "Rajshahi", date))
                    .ReturnsAsync(new List<BusSchedule>());

            var service = new SearchService(mockRepo.Object);

            // Act
            var result = await service.SearchAvailableBusesAsync("Dhaka", "Rajshahi", date);

            // Assert
            result.Should().NotBeNull().And.BeEmpty();

            mockRepo.Verify(r => r.GetSchedulesByRouteAndDateAsync("Dhaka", "Rajshahi", date), Times.Once);
        }
    }
}
