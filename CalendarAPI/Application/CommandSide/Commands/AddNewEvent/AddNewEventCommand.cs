using System;
using MediatR;

namespace CalendarAPI.Application.CommandSide.Commands.AddNewEvent
{
    public class AddNewEventCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int Time { get; set; }
        public string Location { get; set; }
        public string EventOrganizer { get; set; }
        public string[] Members { get; set; }
    }
}