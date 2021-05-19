using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Template_Service.Config.Models;
using Template_Service.Persistence.Entities;

namespace Template_Service.Config.Contexts
{
    public class MongoDbContext : DbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<MongoTemplateEntity> Templates
        {
            get { return _database.GetCollection<MongoTemplateEntity>("Templates"); }
        }
    }
}