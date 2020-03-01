using CalendarAPIMongo.Domain.Models;
using MongoDB.Driver;

namespace CalendarAPIMongo.Infrastructure
{
    public class MongoDbContext
    {
        public readonly IMongoCollection<CalendarEvent> calendarEvents;

        public MongoDbContext(IMongoDatabase database)
        {
            calendarEvents = database.GetCollection<CalendarEvent>("calendarEvents");
        }
    }
}