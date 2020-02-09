using System.Threading;
using System.Threading.Tasks;
using CalendarAPI.Domain.Entity;
using CalendarAPI.Domain.Repositories;
using MediatR;

namespace CalendarAPI.Application.CommandSide.Commands.AddNewEvent
{
    public class AddNewCalendarEventCommandHandler : IRequestHandler<AddNewCalendarEventCommand, int>
    {
        private readonly IEventRepository _eventRepository;

        public AddNewCalendarEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<int> Handle(AddNewCalendarEventCommand command, CancellationToken cancellationToken)
        {
            var calendarEvent = new CalendarEvent(
                command.Name,
                command.Time,
                command.Location,
                command.EventOrganizer
            );
            foreach (var memberName in command.Members)
            {
                calendarEvent.AddMember(memberName);
            }
            
            await _eventRepository.Save(calendarEvent);

            return calendarEvent.Id;
        }
    }
}