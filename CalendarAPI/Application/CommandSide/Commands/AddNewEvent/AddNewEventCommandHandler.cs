using System.Threading;
using System.Threading.Tasks;
using CalendarAPI.Domain.Entity;
using CalendarAPI.Domain.Repositories;
using MediatR;

namespace CalendarAPI.Application.CommandSide.Commands.AddNewEvent
{
    public class AddNewEventCommandHandler : IRequestHandler<AddNewEventCommand, int>
    {
        private readonly IEventRepository _eventRepository;

        public AddNewEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<int> Handle(AddNewEventCommand command, CancellationToken cancellationToken)
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