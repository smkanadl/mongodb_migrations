using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongodb_migrations.Database.Migration
{
    public interface IMigration
    {
        Task Run(BsonDocument document);

        int Version { get; }

        DateTime Date { get; }

        string Target { get; }
    }
}
