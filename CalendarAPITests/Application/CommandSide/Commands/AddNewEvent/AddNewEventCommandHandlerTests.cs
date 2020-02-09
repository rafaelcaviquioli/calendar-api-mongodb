using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPI.Domain.Entity;
using CalendarAPI.Domain.Repositories;
using CalendarAPI.Infrastructure;
using CalendarAPI.Infrastructure.Repositories;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CalendarAPI.Application.CommandSide.Commands.AddNewEvent
{
    public class AddNewEventCommandHandlerTests 
    {
        private readonly IEventRepository _eventRepository;
        private readonly CalendarContext _context;

        public AddNewEventCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<CalendarContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            _context = new CalendarContext(options);
            _eventRepository = new EventRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldInsertNewEventInDatabase_WhenAllPropertiesWasFilled()
        {
            var command = new AddNewEventCommand()
            {
                Name = "Music fetival",
                Location = "Oosterpark, Amsterdam ",
                Time = DateTimeOffset.Now.ToUnixTimeSeconds(),
                EventOrganizer = "Rafael Caviquioli",
                Members = new [] { "Aleida", "Angelique", "Vans" }
            };
            
            var handler = new AddNewEventCommandHandler(_eventRepository);
            var calendarEventId = await handler.Handle(command, CancellationToken.None);

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