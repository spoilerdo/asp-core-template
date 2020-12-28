using Back_End.Config.Models;
using Back_End.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Back_End.Config.Contexts
{
    public class MongoDbContext : DbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<TemplateEntity> Templates
        {
            get { return _database.GetCollection<TemplateEntity>("Templates"); }
        }
    }
}