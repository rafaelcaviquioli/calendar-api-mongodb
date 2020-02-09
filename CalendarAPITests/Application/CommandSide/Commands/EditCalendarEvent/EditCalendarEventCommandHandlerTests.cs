using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarAPI.Application.Exceptions;
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
    public class EditCalendarEventCommandHandlerTests
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly CalendarContext _context;

        public EditCalendarEventCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<CalendarContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            _context = new CalendarContext(options);
            _calendarEventRepository = new CalendarCalendarEventRepository(_context);
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
            _context.Add(calendarEvent);
            await _context.SaveChangesAsync();
            _context.DetachAllEntities();

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

            var updatedCalendarEvent = await _context.CalendarEvents
                .Include(ce => ce.Members)
                .FirstOrDefaultAsync(ce => ce.Id == calendarEvent.Id);

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
                Id = 76890,
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