using System.Collections.Generic;
using System.Threading.Tasks;
using Contract;
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

        public async Task CreateAsync(string id)
        {
            await collection.InsertOneAsync(new Project() { Id = id });
        }
    }
}