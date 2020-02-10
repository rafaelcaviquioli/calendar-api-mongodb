using MediatR;

namespace CalendarAPI.Application.CommandSide.Commands.RemoveCalendarEvent
{
    public class RemoveCalendarEventCommand : IRequest
    {
        public int Id { get; }
        public RemoveCalendarEventCommand(int id)
        {
            Id = id;
        }
    }
}