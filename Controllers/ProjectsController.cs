using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace mongodb_migrations.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        ProjectService service = new ProjectService();
        // GET api/values
        
        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return await service.GetProjectsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Project> Get(string id)
        {
            return await service.GetProjectAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post(string id)
        {
            await service.CreateProjectAsync(id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
