using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentManagementApp.Models
{
    public class StudentInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string Batch { get; set; } = null!;
        public string CollegeName { get; set; } = null!;
    }
}
