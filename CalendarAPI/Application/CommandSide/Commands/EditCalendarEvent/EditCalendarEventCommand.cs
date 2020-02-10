using MediatR;

namespace CalendarAPI.Application.CommandSide.Commands.EditCalendarEvent
{
    public class EditCalendarEventCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Time { get; set; }
        public string Location { get; set; }
        public string EventOrganizer { get; set; }
        public string[] Members { get; set; }
    }
}