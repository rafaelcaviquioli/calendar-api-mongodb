using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Domain.Entity;
using CalendarAPIMongo.Domain.Repositories;
using MediatR;

namespace CalendarAPIMongo.Application.CommandSide.Commands.AddNewCalendarEvent
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