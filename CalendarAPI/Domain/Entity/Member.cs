using System.ComponentModel.DataAnnotations;

namespace CalendarAPI.Domain.Entity
{
    public class Member
    {
        public int Id { get; private set; }
        [Required]
        public int EventId { get; private set; }
        [Required]
        public string Name { get; private set; }
        public Event Event { get; private set; }
    }
}