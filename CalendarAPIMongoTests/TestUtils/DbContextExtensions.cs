using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CalendarAPITests.TestUtils
{
    public static class DbContextExtensions
    {
        public static void DetachAllEntities(this DbContext context)
        {
            var safeEntriesThatWontAffectTheOriginalList = context.ChangeTracker.Entries().ToArray();
            foreach (var entry in safeEntriesThatWontAffectTheOriginalList)
            {
                if (entry.Entity != null)
                {
                    entry.State = EntityState.Detached;
                }
            }
        }
    }
}