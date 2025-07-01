using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentManagementApp.Models;

namespace StudentManagementApp.Services
{
    public class StudentInfoService
    {
        private readonly IMongoCollection<StudentInfo> _students;

        public StudentInfoService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionURI);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _students = database.GetCollection<StudentInfo>(settings.Value.StudentCollectionName);
        }

        public async Task CreateStudentAsync(StudentInfo student)
        {
            await _students.InsertOneAsync(student);
        }

        public async Task<List<StudentInfo>> GetAllStudentsAsync()
        {
            return await _students.Find(_ => true).ToListAsync();
        }
        public async Task DeleteStudentAsync(string id)
{
    await _students.DeleteOneAsync(s => s.Id == id);
}

public async Task<Dictionary<string, int>> GetSummaryByCourseAsync()
{
    var summaryList = await _students
        .Aggregate()
        .Group(s => s.CourseName, g => new { Course = g.Key, Count = g.Count() })
        .ToListAsync();

    return summaryList.ToDictionary(x => x.Course, x => x.Count);
}


public async Task<int> GetTotalCountAsync()
{
    return (int)await _students.CountDocumentsAsync(_ => true);
}

    }
}
