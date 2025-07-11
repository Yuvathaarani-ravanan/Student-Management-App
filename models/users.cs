using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentManagementApp.Models;
{
   public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }

    public string? OtpCode { get; set; }
    public DateTime? OtpExpiry { get; set; }
    public bool IsVerified { get; set; } = false;
}

}
