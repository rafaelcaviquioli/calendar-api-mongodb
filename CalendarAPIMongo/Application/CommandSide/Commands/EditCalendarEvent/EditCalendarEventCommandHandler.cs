using System.Threading;
using System.Threading.Tasks;
using CalendarAPIMongo.Application.Exceptions;
using CalendarAPIMongo.Domain.Repositories;
using MediatR;

namespace CalendarAPIMongo.Application.CommandSide.Commands.EditCalendarEvent
{
    public class EditCalendarEventCommandHandler : IRequestHandler<EditCalendarEventCommand>
    {
        private readonly ICalendarEventRepository _calendarEventRepository;

        public EditCalendarEventCommandHandler(ICalendarEventRepository calendarEventRepository)
        {
            _calendarEventRepository = calendarEventRepository;
        }

        public async Task<Unit> Handle(EditCalendarEventCommand command, CancellationToken cancellationToken)
        {
            var calendarEvent = await _calendarEventRepository.FindOne(command.Id);
            
            if(calendarEvent == null)
                throw new ResourceNotFoundException();
            
            calendarEvent.Edit(
                command.Name,
                command.Time,
                command.Location,
                command.EventOrganizer
            );
            calendarEvent.EditMembers(command.Members);
            
            await _calendarEventRepository.Update(calendarEvent);

            return Unit.Value;
        }
    }
}