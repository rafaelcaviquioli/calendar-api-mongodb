using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.CommandSide.Commands.AddNewCalendarEvent;
using CalendarAPIMongo.Domain.Repositories;
using CalendarAPIMongo.Infrastructure;
using CalendarAPIMongo.Infrastructure.Repositories;
using FluentAssertions;
using Mongo2Go;
using Xunit;
using MongoDB.Driver;

namespace CalendarAPITests.Application.CommandSide.Commands.AddNewCalendarEvent
{
    public class AddNewCalendarEventCommandHandlerTests 
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly MongoDbContext _context;

        public AddNewCalendarEventCommandHandlerTests()
        {
            var runner = MongoDbRunner.Start(singleNodeReplSet: true, singleNodeReplSetWaitTimeout: 10);
            var server = new MongoClient(runner.ConnectionString);

            var database = server.GetDatabase("inMemoryDatabase");
            _context = new MongoDbContext(database);
            _calendarEventRepository = new CalendarEventRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldInsertNewEventInDatabase_WhenAllPropertiesWasFilled()
        {
            var command = new AddNewCalendarEventCommand()
            {
                Name = "Music fetival",
                Location = "Oosterpark, Amsterdam ",
                Time = DateTimeOffset.Now.ToUnixTimeSeconds(),
                EventOrganizer = "Rafael Caviquioli",
                Members = new [] { "Aleida", "Angelique", "Vans" }
            };
            
            var handler = new AddNewCalendarEventCommandHandler(_calendarEventRepository);
            var calendarEventId = await handler.Handle(command, CancellationToken.None);
            
            var calendarEvent = await _context.calendarEvents
                .Find(ce => ce.Id == calendarEventId)
                .FirstOrDefaultAsync();

            calendarEvent.Name.Should().Be(command.Name);
            calendarEvent.Location.Should().Be(command.Location);
            calendarEvent.Time.Should().Be(command.Time);
            calendarEvent.Organizer.Should().Be(command.EventOrganizer);
            calendarEvent.Members.Should().HaveCount(3);
            var members = calendarEvent.Members.ToList();
            members[0].Name.Should().Be(command.Members[0]);
            members[1].Name.Should().Be(command.Members[1]);
            members[2].Name.Should().Be(command.Members[2]);
        }
    }
}