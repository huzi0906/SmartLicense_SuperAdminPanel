using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SmartLicense_SuperAdminSide.Models;

namespace SmartLicense_SuperAdminSide.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Admin> Admins => _database.GetCollection<Admin>("Admins");
    }
}