using System;
using MediatR;

namespace CalendarAPI.Application.CommandSide.Commands.AddNewEvent
{
    public class AddNewEventCommand : IRequest<int>
    {
        public string Name { get; }
        public DateTime Time { get; }
        public string Location { get; }
        public string Organizer { get; }
        public string[] Members { get; }

        public AddNewEventCommand(string name, DateTime time, string location, string organizer, string[] members)
        {
            Name = name;
            Time = time;
            Location = location;
            Organizer = organizer;
            Members = members;
        }
    }
}