using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalendarAPI.Domain.Entity
{
    public class Event
    {
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public DateTime Time { get; private set; }
        [Required]
        public string Location { get; private set; }
        [Required]
        public string Organizer { get; private set; }
        public ICollection<Member> Members { get; set; }
    }
}