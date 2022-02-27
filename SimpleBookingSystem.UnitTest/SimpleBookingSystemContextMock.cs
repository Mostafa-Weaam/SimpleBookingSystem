using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SimpleBookingSystemService.Infrastructure;
using System;
using System.Collections.Generic;

namespace SimpleBookingSystem.UnitTest
{
    public static class SimpleBookingSystemContextMock
    {
        public static SimpleBookingSystemContext GetSimpleBookingSystemContext()
        {
            var options = new DbContextOptionsBuilder<SimpleBookingSystemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var simpleBookingSystemContext = new SimpleBookingSystemContext(options);
            simpleBookingSystemContext.Database.EnsureCreated();

            SeedResourceRecords(simpleBookingSystemContext);
            simpleBookingSystemContext.SaveChanges();

            foreach (var dbEntityEntry in simpleBookingSystemContext.ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
            return simpleBookingSystemContext;
        }

        private static void SeedResourceRecords(SimpleBookingSystemContext simpleBookingSystemContext)
        {
            var resources = new List<SimpleBookingSystemService.Infrastructure.Domain.Resource>();
            resources.Add(new SimpleBookingSystemService.Infrastructure.Domain.Resource { Id = 1, Name = "Resource 1", Quantity = 100 });

            simpleBookingSystemContext.Resource.AddRange(resources);
        }
    }
}
