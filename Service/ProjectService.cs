using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract;
using Model;

namespace Service
{
    public class ProjectService
    {
        ProjectRepository repository;
        
        public ProjectService()
        {
            repository = new ProjectRepository();
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await repository.GetAsync();
        }

        public async Task<Project> CreateProjectAsync()
        {
            return await repository.CreateAsync();
        }

        public async Task<Project> GetProjectAsync(string id)
        {
            return await repository.GetAsync(id);
        }
    }
}