using MongoDB.Driver;
using poembook.Models;

namespace poembook.Data
{
    public interface IMongoDbContext
    {
        IMongoDatabase _database { get; }
    }
}