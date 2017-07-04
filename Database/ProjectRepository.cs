using System.Collections.Generic;
using System.Threading.Tasks;
using Contract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using mongodb_migrations.Database.Migration;

namespace Model
{
    public class ProjectRepository : RepositoryBase<Project>
    {
        private CollectionMigrationRunner runner;

        public ProjectRepository()
            : base("projects")
        {
            runner = new CollectionMigrationRunner("projects");
        }

        public async Task<List<Project>> GetAsync()
        {
            return await collection.Find(d => true).ToListAsync();
        }

        public async Task<Project> GetAsync(string id)
        {
            var c = collection.Database.GetCollection<BsonDocument>("projects");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var project = await c.Find(filter).FirstOrDefaultAsync();

            await runner.Run(project);

            return BsonSerializer.Deserialize<Project>(project);
        }
        
        public async Task<Project> CreateAsync()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var project = new Project()
            {
                Id = id,
                Name = "Project for id " + id,
                System = new Contract.System()
                {
                    Elements = new List<Element>()
                    {
                        new Element() { Id = ObjectId.GenerateNewId().ToString(), Name = "my element" }
                    }
                }
            };
            await collection.InsertOneAsync(project);
            return project;
        }
    }
}