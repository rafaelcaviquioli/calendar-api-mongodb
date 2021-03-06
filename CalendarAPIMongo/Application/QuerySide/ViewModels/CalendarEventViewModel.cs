namespace CalendarAPIMongo.Application.QuerySide.ViewModels
{
    public class CalendarEventViewModel
    {
        public CalendarEventViewModel(string id, string name, long time, string location, string eventOrganizer, string[] members)
        {
            Id = id;
            Name = name;
            Time = time;
            Location = location;
            EventOrganizer = eventOrganizer;
            Members = string.Join(",", members); 
        }

        public string Id { get; }
        public string Name { get; }
        public long Time { get; }
        public string Location { get; }
        public string EventOrganizer { get; }
        public string Members { get; }
    }
}