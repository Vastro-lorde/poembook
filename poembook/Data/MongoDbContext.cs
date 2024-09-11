using MongoDB.Driver;
using poembook.Models;

namespace poembook.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoDatabase _database { get; }

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
    }
}
