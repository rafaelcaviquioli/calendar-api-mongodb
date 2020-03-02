using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.CommandSide.Commands.EditCalendarEvent;
using CalendarAPIMongo.Application.Exceptions;
using CalendarAPIMongo.Domain.Models;
using CalendarAPIMongo.Domain.Repositories;
using CalendarAPIMongo.Infrastructure;
using CalendarAPIMongo.Infrastructure.Repositories;
using FluentAssertions;
using Mongo2Go;
using MongoDB.Driver;
using Xunit;

namespace CalendarAPITests.Application.CommandSide.Commands.EditCalendarEvent
{
    public class EditCalendarEventCommandHandlerTests
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly MongoDbContext _context;

        public EditCalendarEventCommandHandlerTests()
        {
            var runner = MongoDbRunner.Start(singleNodeReplSet: true, singleNodeReplSetWaitTimeout: 10);
            var server = new MongoClient(runner.ConnectionString);

            var database = server.GetDatabase("inMemoryDatabase");
            _context = new MongoDbContext(database);
            _calendarEventRepository = new CalendarEventRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldEditCalendarEventDataInDatabase()
        {
            var calendarEvent = new CalendarEvent(
                "Music fetival",
                DateTimeOffset.Now.ToUnixTimeSeconds(),
                "Oosterpark, Amsterdam ",
                "Rafael Caviquioli"
            );
            calendarEvent.AddMember("Aleida");
            calendarEvent.AddMember("Vans Martin");
            await _context.calendarEvents.InsertOneAsync(calendarEvent);

            var command = new EditCalendarEventCommand()
            {
                Id = calendarEvent.Id,
                Name = "Football Challenge",
                Time = DateTimeOffset.Now.AddDays(10).ToUnixTimeSeconds(),
                Location = "Brussels, Belgian",
                EventOrganizer = "Elisabeth",
                Members = new string[] {"Gabriel", "Anna", "Aleida", "Felipe"}
            };
            var handler = new EditCalendarEventCommandHandler(_calendarEventRepository);
            await handler.Handle(command, CancellationToken.None);

            var updatedCalendarEvent = await _context.calendarEvents
                .Find(ce => ce.Id == calendarEvent.Id)
                .FirstOrDefaultAsync();

            updatedCalendarEvent.Name.Should().Be(command.Name);
            updatedCalendarEvent.Location.Should().Be(command.Location);
            updatedCalendarEvent.Time.Should().Be(command.Time);
            updatedCalendarEvent.Organizer.Should().Be(command.EventOrganizer);
            updatedCalendarEvent.Members.Should().HaveCount(4);
            
            var members = updatedCalendarEvent.Members
                .Select(m => m.Name)
                .ToList();
            
            members.Should().Contain("Gabriel");
            members.Should().Contain("Anna");
            members.Should().Contain("Aleida");
            members.Should().Contain("Felipe");
            members.Should().NotContain("Vans Martin");
        }
        
        [Fact]
        public async Task Handle_ShouldThrowResourceNotFoundException_WhenTryToUpdateACalendarEventThatDoesNotExists()
        {
            var command = new EditCalendarEventCommand()
            {
                Id = "5e5c341ad9978660124045f6",
                Name = "Football Challenge",
                Time = DateTimeOffset.Now.AddDays(10).ToUnixTimeSeconds(),
                Location = "Brussels, Belgian",
                EventOrganizer = "Elisabeth",
                Members = new string[] {"Gabriel", "Anna", "Aleida", "Felipe"}
            };
            var handler = new EditCalendarEventCommandHandler(_calendarEventRepository);

            await Assert.ThrowsAsync<ResourceNotFoundException>(
                async () => await handler.Handle(command, CancellationToken.None)
            );
        }
    }
}