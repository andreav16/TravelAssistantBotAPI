using Google.Apis.Calendar.v3.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.EventManager;

namespace TravelAssistantBot.Tests
{
    public class EventTests
    {
        [Fact]
        public async Task GetEventByName_ValidName_ReturnCorrectEvent()
        {
            // Arrange
            var eventServiceMock = new Mock<IEventService>();
            eventServiceMock.Setup(e => e.GetEventByNameAsync("Go to the doctor")).ReturnsAsync(new Event
            {
                Id = "1",
                Summary = "Go to the doctor",
                Start = new EventDateTime { DateTime = DateTime.Now.AddDays(1) },
                End = new EventDateTime { DateTime = DateTime.Now.AddDays(2) }
            });

            var eventService = eventServiceMock.Object;

            // Act
            var result = await eventService.GetEventByNameAsync("Go to the doctor");

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Go to the doctor", result.Result.Summary);
        }
    }
}
