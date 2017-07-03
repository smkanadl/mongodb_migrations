using MongoDB.Bson;
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
        
        public MigrationRunner(string collection)
        {
            var current = Assembly.GetEntryAssembly();
            migrations = CreateInstancesByType<IMigration>(current)
                .Where(c => c.Target == collection)
                .OrderBy(m => m.Date);
        }

        static IEnumerable<T> CreateInstancesByType<T>(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract)
                .Select(Activator.CreateInstance)
                .OfType<T>();
        }

        public async Task Run(BsonDocument doc)
        {
            var currentVersion = doc.Contains("version") ? doc["version"].AsInt32 : 0;
            if (currentVersion < Contract.Version.Current)
            {
                var missing = migrations.Where(m => m.Version > currentVersion);
                foreach (var item in missing)
                {
                    await item.Run(doc);
                }
            }
        }
    }
}
