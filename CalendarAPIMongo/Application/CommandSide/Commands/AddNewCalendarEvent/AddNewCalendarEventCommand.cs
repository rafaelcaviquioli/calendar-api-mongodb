using MediatR;

namespace CalendarAPIMongo.Application.CommandSide.Commands.AddNewCalendarEvent
{
    public class AddNewCalendarEventCommand : IRequest<string>
    {
        public string Name { get; set; }
        public long Time { get; set; }
        public string Location { get; set; }
        public string EventOrganizer { get; set; }
        public string[] Members { get; set; }
    }
}