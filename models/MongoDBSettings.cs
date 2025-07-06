namespace StudentManagementApp.Models;
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string UserCollectionName { get; set; } = string.Empty;
        public string TaskCollectionName { get; set; } = string.Empty;
        public string StudentCollectionName { get; set; } = string.Empty;

    }
}
