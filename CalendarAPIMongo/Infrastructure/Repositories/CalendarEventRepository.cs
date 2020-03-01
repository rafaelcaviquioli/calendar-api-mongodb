using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarAPIMongo.Domain.Models;
using CalendarAPIMongo.Domain.Repositories;
using MongoDB.Driver;

namespace CalendarAPIMongo.Infrastructure.Repositories
{
    public class CalendarEventRepository : ICalendarEventRepository
    {
        private readonly IMongoCollection<CalendarEvent> _calendarEvents;

        public CalendarEventRepository(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _calendarEvents = database.GetCollection<CalendarEvent>(settings.CalendarEventsCollectionName);
        }

        public async Task Update(CalendarEvent calendarEventIn)
            => await _calendarEvents.ReplaceOneAsync(
                calendarEvent => calendarEvent.Id == calendarEventIn.Id,
                calendarEventIn
            );

        public async Task Insert(CalendarEvent calendarEventIn)
            => await _calendarEvents.InsertOneAsync(calendarEventIn);

        public async Task<CalendarEvent> FindOne(string id) =>
            await _calendarEvents
                .Find<CalendarEvent>(calendarEvent => calendarEvent.Id == id)
                .FirstOrDefaultAsync();

        public async Task Remove(CalendarEvent calendarEventIn) =>
            await _calendarEvents.DeleteOneAsync(calendarEvent => calendarEvent.Id == calendarEventIn.Id);

        public IEnumerable<CalendarEvent> List() =>
            _calendarEvents.AsQueryable<CalendarEvent>();
    }
}