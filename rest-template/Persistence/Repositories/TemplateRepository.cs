using System.Threading.Tasks;
using Back_End.Config.Contexts;
using Back_End.Config.Models;
using Back_End.Persistence.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Back_End.Persistence.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly MongoDbContext _context;

        public TemplateRepository(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<TemplateEntity> GetTemplateById(string id)
        {
            return await _context.Templates.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TemplateEntity> Add(TemplateEntity template)
        {
            await _context.Templates.InsertOneAsync(template);
            return template;
        }
        public async Task<TemplateEntity> Update(TemplateEntity template)
        {
            var filter = Builders<TemplateEntity>.Filter.Eq(t => t.Id, template.Id);
            await _context.Templates.FindOneAndReplaceAsync<TemplateEntity>(filter, template);
            return template;
        }

        public async Task Delete(string id)
        {
            var filter = Builders<TemplateEntity>.Filter.Eq("_Id", id);
            await _context.Templates.DeleteOneAsync(filter);
        }

    }
}