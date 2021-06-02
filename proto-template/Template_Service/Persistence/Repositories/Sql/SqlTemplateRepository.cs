using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Template_Service.Config.Contexts;
using Template_Service.Persistence.Entities;

namespace Template_Service.Persistence.Repositories.Sql {
    public class SqlTemplateRepository : ISqlTemplateRepository {
        private readonly MySqlDbContext _context;

        public SqlTemplateRepository(MySqlDbContext context) {
            this._context = context;
        }

        public async Task<SqlTemplateEntity> Add(SqlTemplateEntity template) {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();
            return template;
        }

        public async Task Delete(Guid id) {
            var template = await _context.Templates.Where(t => t.Id == id).FirstOrDefaultAsync();
            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
        }

        public async Task<SqlTemplateEntity> GetTemplateById(Guid id) {
            return await _context.Templates.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SqlTemplateEntity> Update(SqlTemplateEntity template) {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();
            return template;
        }
    }
}