using System;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.CommandSide.Commands.RemoveCalendarEvent;
using CalendarAPIMongo.Application.Exceptions;
using CalendarAPIMongo.Domain.Models;
using CalendarAPIMongo.Domain.Repositories;
using CalendarAPIMongo.Infrastructure;
using CalendarAPIMongo.Infrastructure.Repositories;
using FluentAssertions;
using Mongo2Go;
using MongoDB.Driver;
using Xunit;

namespace CalendarAPITests.Application.CommandSide.Commands.RemoveCalendarEvent
{
    public class DeleteCalendarEventCommandHandlerTests
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly MongoDbContext _context;

        public DeleteCalendarEventCommandHandlerTests()
        {
            var runner = MongoDbRunner.Start(singleNodeReplSet: true, singleNodeReplSetWaitTimeout: 10);
            var server = new MongoClient(runner.ConnectionString);

            var database = server.GetDatabase("inMemoryDatabase");
            _context = new MongoDbContext(database);
            _calendarEventRepository = new CalendarEventRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldThrowResourceNotFoundException_WhenTryToRemoveACalendarEventThatDoesNotExists()
        {
            var command = new RemoveCalendarEventCommand("5e5c341ad9978660124015f6");
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
            await _context.calendarEvents.InsertOneAsync(calendarEvent);

            var command = new RemoveCalendarEventCommand(calendarEvent.Id);
            var handler = new RemoveCalendarEventCommandHandler(_calendarEventRepository);
            await handler.Handle(command, CancellationToken.None);

            var calendarEventExpected = await _context.calendarEvents
                .Find(ce => ce.Id == calendarEvent.Id)
                .FirstOrDefaultAsync();

            calendarEventExpected
                .Should().BeNull("Because calendar event was removed from database");
        }
    }
}