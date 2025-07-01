namespace StudentManagementApp.Models
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
        public string UserCollectionName { get; set; }
        public string TaskCollectionName { get; set; }
        public string StudentCollectionName { get; set; } = null!;

    }
}
