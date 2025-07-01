using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentManagementApp.Models
{
    public class TaskModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string TaskName { get; set; }
        public string TaskStatus { get; set; } // "On Progress" or "Completed"
        public string StudentName { get; set; }
    }
}
