using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Model
{
    public class Project
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class Repository
    {
        MongoClient client;
        
        IMongoDatabase db;

        public Repository()
        {
            client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("test");
        }

        public async Task<List<Project>> GetAsync()
        {
            var collection = db.GetCollection<Project>("projects");
            return await collection.Find(d => true).ToListAsync();
        }
    } 
}