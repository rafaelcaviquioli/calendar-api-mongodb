using System.Threading;
using System.Threading.Tasks;
using CalendarAPI.Application.Exceptions;
using CalendarAPI.Domain.Entity;
using CalendarAPI.Domain.Repositories;
using MediatR;

namespace CalendarAPI.Application.CommandSide.Commands.AddNewEvent
{
    public class RemoveCalendarEventCommandHandler : IRequestHandler<RemoveCalendarEventCommand>
    {
        private readonly ICalendarEventRepository _calendarEventRepository;

        public RemoveCalendarEventCommandHandler(ICalendarEventRepository calendarEventRepository)
        {
            _calendarEventRepository = calendarEventRepository;
        }

        public async Task<Unit> Handle(RemoveCalendarEventCommand command, CancellationToken cancellationToken)
        {
            var calendarEvent = await _calendarEventRepository.FindOne(command.Id);
            
            if(calendarEvent == null)
                throw new ResourceNotFoundException();
            
            await _calendarEventRepository.Remove(calendarEvent);
            
            return Unit.Value;
        }
    }
}