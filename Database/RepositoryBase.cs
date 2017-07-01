using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Model
{
    public class RepositoryBase<T>
    {
        static MongoClient client;
        
        static IMongoDatabase db;

        protected IMongoCollection<T> collection;

        static RepositoryBase()
        {
            client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("test");
        }

        protected RepositoryBase(string collection)
        {
            this.collection = db.GetCollection<T>(collection);
        }
    }
}