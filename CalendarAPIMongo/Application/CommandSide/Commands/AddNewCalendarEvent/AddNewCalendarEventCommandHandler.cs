using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Domain.Models;
using CalendarAPIMongo.Domain.Repositories;
using MediatR;

namespace CalendarAPIMongo.Application.CommandSide.Commands.AddNewCalendarEvent
{
    public class AddNewCalendarEventCommandHandler : IRequestHandler<AddNewCalendarEventCommand, string>
    {
        private readonly ICalendarEventRepository _calendarEventRepository;

        public AddNewCalendarEventCommandHandler(ICalendarEventRepository calendarEventRepository)
        {
            _calendarEventRepository = calendarEventRepository;
        }

        public async Task<string> Handle(AddNewCalendarEventCommand command, CancellationToken cancellationToken)
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
            
            await _calendarEventRepository.Insert(calendarEvent);

            return calendarEvent.Id;
        }
    }
}