using System.Threading;
using System.Threading.Tasks;
using CalendarAPI.Domain.Entity;
using CalendarAPI.Domain.Repositories;
using MediatR;

namespace CalendarAPI.Application.CommandSide.Commands.AddNewEvent
{
    public class AddNewCalendarEventCommandHandler : IRequestHandler<AddNewCalendarEventCommand, int>
    {
        private readonly ICalendarEventRepository _calendarEventRepository;

        public AddNewCalendarEventCommandHandler(ICalendarEventRepository calendarEventRepository)
        {
            _calendarEventRepository = calendarEventRepository;
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
            
            await _calendarEventRepository.Save(calendarEvent);

            return calendarEvent.Id;
        }
    }
}