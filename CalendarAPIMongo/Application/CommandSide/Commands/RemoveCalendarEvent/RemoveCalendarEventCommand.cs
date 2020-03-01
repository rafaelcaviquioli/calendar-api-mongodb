using MediatR;

namespace CalendarAPIMongo.Application.CommandSide.Commands.RemoveCalendarEvent
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