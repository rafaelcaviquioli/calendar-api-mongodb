using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CalendarAPI.Domain.Entity
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public long Time { get; set; }
        [Required] public string Location { get; set; }
        [Required] public string Organizer { get; set; }

        public virtual ICollection<Member> Members { get; private set; } = new List<Member>();

        public CalendarEvent(string name, long time, string location, string organizer)
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

        public void EditMembers(string[] newMembers)
        {
            var currentMembers = Members.Select(m => m.Name);
            var removedMembers = currentMembers.Except(newMembers).ToList();
            var addedMembers = newMembers.Except(currentMembers).ToList();

            foreach (var removedMemberName in removedMembers)
                Members.Remove(Members.First(m => m.Name == removedMemberName));

            foreach (var addedMemberName in addedMembers)
                AddMember(addedMemberName);
        }
        public void Edit(string name, long time, string location, string organizer)
        {
            Name = name;
            Time = time;
            Location = location;
            Organizer = organizer;
        }
    }
}