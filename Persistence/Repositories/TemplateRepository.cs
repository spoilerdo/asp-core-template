using System.Threading.Tasks;
using Back_End.Config.Contexts;
using Back_End.Config.Models;
using Back_End.Persistence.Common;
using Back_End.Persistence.Entities;
using Microsoft.Extensions.Options;

namespace Back_End.Persistence.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly MongoDbContext _context;

        public TemplateRepository(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<DataResponseObject<TemplateEntity>> Add(TemplateEntity template)
        {
            await _context.Templates.InsertOneAsync(template);
            return new DataResponseObject<TemplateEntity>(template);
        }

        Task<DataResponseObject<TemplateEntity>> ITemplateRepository.GetTemplateById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}