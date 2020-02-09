using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalendarAPI.Domain.Entity
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Organizer { get; set; }

        public virtual ICollection<Member> Members { get; private set; } = new List<Member>();
        
        public CalendarEvent(string name, int time, string location, string organizer)
        {
            Name = name;
            Time = time;
            Location = location;
            Organizer = organizer;
        }

        public void AddMember(string name)
        {
            Members.Add(new Member(name));
        }
    }
}