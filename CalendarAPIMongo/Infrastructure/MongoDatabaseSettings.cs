namespace CalendarAPIMongo.Infrastructure
{
    public class MongoDatabaseSettings: IMongoDatabaseSettings
    {
        public string CalendarEventsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        string CalendarEventsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}