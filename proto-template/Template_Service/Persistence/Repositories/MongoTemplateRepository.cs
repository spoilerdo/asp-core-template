using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Template_Service.Config.Contexts;
using Template_Service.Config.Models;
using Template_Service.Persistence.Entities;

namespace Template_Service.Persistence.Repositories
{
    public class MongoTemplateRepository : IMongoTemplateRepository
    {
        private readonly MongoDbContext _context;

        public MongoTemplateRepository(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<MongoTemplateEntity> GetTemplateById(string id)
        {
            return await _context.Templates.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<MongoTemplateEntity> Add(MongoTemplateEntity template)
        {
            await _context.Templates.InsertOneAsync(template);
            return template;
        }
        public async Task<MongoTemplateEntity> Update(MongoTemplateEntity template)
        {
            var filter = Builders<MongoTemplateEntity>.Filter.Eq(t => t.Id, template.Id);
            await _context.Templates.FindOneAndReplaceAsync<MongoTemplateEntity>(filter, template);
            return template;
        }

        public async Task Delete(string id)
        {
            var filter = Builders<MongoTemplateEntity>.Filter.Eq("_Id", id);
            await _context.Templates.DeleteOneAsync(filter);
        }

    }
}