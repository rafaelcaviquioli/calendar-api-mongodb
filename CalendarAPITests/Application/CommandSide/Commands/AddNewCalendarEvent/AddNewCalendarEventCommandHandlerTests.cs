using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPI.Domain.Entity;
using CalendarAPI.Domain.Repositories;
using CalendarAPI.Infrastructure;
using CalendarAPI.Infrastructure.Repositories;
using CalendarAPITests.TestUtils;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CalendarAPI.Application.CommandSide.Commands.AddNewEvent
{
    public class AddNewCalendarEventCommandHandlerTests 
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly CalendarContext _context;

        public AddNewCalendarEventCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<CalendarContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            _context = new CalendarContext(options);
            _calendarEventRepository = new CalendarCalendarEventRepository(_context);
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
            _context.DetachAllEntities();
            
            var calendarEvent = await _context.CalendarEvents
                .Include(ce => ce.Members)
                .FirstOrDefaultAsync(ce => ce.Id == calendarEventId);

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