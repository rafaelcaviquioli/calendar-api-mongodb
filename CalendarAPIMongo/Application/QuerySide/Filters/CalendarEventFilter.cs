namespace CalendarAPIMongo.Application.QuerySide.Filters
{
    public class CalendarEventFilter
    {
        public string EventOrganizer { get; set; }
        public string Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
    }
}