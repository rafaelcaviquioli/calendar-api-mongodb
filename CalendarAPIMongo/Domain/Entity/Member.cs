using System.ComponentModel.DataAnnotations;

namespace CalendarAPIMongo.Domain.Entity
{
    public class Member
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public Member(string name)
        {
            Name = name;
        }
    }
}