using MediatR;

namespace CalendarAPIMongo.Application.CommandSide.Commands.RemoveCalendarEvent
{
    public class RemoveCalendarEventCommand : IRequest
    {
        public string Id { get; }
        public RemoveCalendarEventCommand(string id)
        {
            Id = id;
        }
    }
}