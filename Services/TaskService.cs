using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentManagementApp.Models;

namespace StudentManagementApp.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<TaskModel> _tasks;

        public TaskService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionURI);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _tasks = database.GetCollection<TaskModel>(settings.Value.TaskCollectionName);
        }

        public async Task<List<TaskModel>> GetAllAsync() =>
            await _tasks.Find(_ => true).ToListAsync();

        public async Task CreateAsync(TaskModel task) =>
            await _tasks.InsertOneAsync(task);
    }
}
