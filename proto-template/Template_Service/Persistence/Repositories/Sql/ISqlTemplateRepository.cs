using System;
using System.Threading.Tasks;
using Template_Service.Persistence.Entities;

namespace Template_Service.Persistence.Repositories.Sql {
    public interface ISqlTemplateRepository {
        Task<SqlTemplateEntity> GetTemplateById(Guid id);
        Task<SqlTemplateEntity> Add(SqlTemplateEntity template);
        Task<SqlTemplateEntity> Update(SqlTemplateEntity template);
        Task Delete(Guid id);
    }
}