using mongodb_migrations.Database.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace mongodb_migrations.Migrations
{
    public class Migration_1 : IMigration
    {
        public int Version => 1;

        public DateTime Date => new DateTime(2017, 07, 03, 10, 30, 00);

        public string Target => "projects";

        public Task Run(BsonDocument document)
        {
            return Task.Run(() =>
            {
                document["version"] = 1;
            });
        }
    }
}
