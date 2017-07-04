using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace mongodb_migrations.Database.Migration
{
    public class MigrationRunner
    {
        private IEnumerable<IMigration> migrations;

        public MigrationRunner()
        {
            var current = Assembly.GetEntryAssembly();
            migrations = CreateInstancesByType<IMigration>(current)
                .OrderBy(m => m.Date);
        }

        static IEnumerable<T> CreateInstancesByType<T>(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract)
                .Select(Activator.CreateInstance)
                .OfType<T>();
        }

        public bool IsRunning { get; private set; }

        public async Task Run()
        {
            IsRunning = true;
            try
            {
                var client = new MongoClient();
                var db = client.GetDatabase("test");

                // TODO: Filter migrations to avoid addtional workload
                
                foreach (var item in migrations)
                {
                    var collection = db.GetCollection<BsonDocument>(item.Target);
                    await collection.Find(d => true).ForEachAsync(async d =>
                    {
                        await Task.Delay(5000);
                        await item.Run(d);
                        await collection.ReplaceOneAsync(
                            Builders<BsonDocument>.Filter.Eq("_id", d["_id"]), d);
                    });
                }
            }
            finally
            {
                IsRunning = false;
            }
        }
    }
}
