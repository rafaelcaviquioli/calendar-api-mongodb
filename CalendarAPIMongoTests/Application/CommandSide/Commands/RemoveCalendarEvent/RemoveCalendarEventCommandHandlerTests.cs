using System;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.CommandSide.Commands.RemoveCalendarEvent;
using CalendarAPIMongo.Application.Exceptions;
using CalendarAPIMongo.Domain.Entity;
using CalendarAPIMongo.Domain.Repositories;
using CalendarAPIMongo.Infrastructure;
using CalendarAPIMongo.Infrastructure.Repositories;
using CalendarAPITests.TestUtils;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CalendarAPITests.Application.CommandSide.Commands.RemoveCalendarEvent
{
    public class DeleteCalendarEventCommandHandlerTests
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly CalendarContext _context;

        public DeleteCalendarEventCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<CalendarContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            _context = new CalendarContext(options);
            _calendarEventRepository = new CalendarCalendarEventRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldThrowResourceNotFoundException_WhenTryToRemoveACalendarEventThatDoesNotExists()
        {
            var command = new RemoveCalendarEventCommand(1234556789);
            var handler = new RemoveCalendarEventCommandHandler(_calendarEventRepository);

            await Assert.ThrowsAsync<ResourceNotFoundException>(
                async () => await handler.Handle(command, CancellationToken.None)
            );
        }

        [Fact]
        public async Task Handle_RemoveCalendarEvent_WhenItExists()
        {
            var calendarEvent = new CalendarEvent(
                "Music fetival",
                DateTimeOffset.Now.ToUnixTimeSeconds(),
                "Oosterpark, Amsterdam ",
                "Rafael Caviquioli"
            );
            calendarEvent.AddMember("Aleida");
            _context.Add(calendarEvent);
            await _context.SaveChangesAsync();
            _context.DetachAllEntities();

            var command = new RemoveCalendarEventCommand(calendarEvent.Id);
            var handler = new RemoveCalendarEventCommandHandler(_calendarEventRepository);
            await handler.Handle(command, CancellationToken.None);

            var calendarEventExpected = await _context.CalendarEvents
                .FirstOrDefaultAsync(ce => ce.Id == calendarEvent.Id);

            calendarEventExpected
                .Should().BeNull("Because calendar event was removed from database");
        }
    }
}