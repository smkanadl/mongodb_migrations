using System.Collections.Generic;
using System.Threading.Tasks;
using Contract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Model
{
    public class ProjectRepository : RepositoryBase<Project>
    {
        public ProjectRepository()
            : base("projects")
        {   
        }

        public async Task<List<Project>> GetAsync()
        {
            return await collection.Find(d => true).ToListAsync();
        }

        public async Task<Project> GetAsync(string id)
        {
            var c = collection.Database.GetCollection<BsonDocument>("projects");
            var filter = Builders<BsonDocument>.Filter.Eq("Id", id);
            var project = await c.Find(filter).FirstOrDefaultAsync();
            var version = project["version"].AsInt32;
            if(version < Version.Current)
            {
                // run migration
            }
            return BsonSerializer.Deserialize<Project>(project);
        }

        public async Task CreateAsync(string id)
        {
            await collection.InsertOneAsync(new Project() { Id = id });
        }
    }
}