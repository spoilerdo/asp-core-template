using System.Threading.Tasks;
using Template_Service.Persistence.Entities;

namespace Template_Service.Persistence.Repositories {
    public interface IMongoTemplateRepository {
        Task<MongoTemplateEntity> GetTemplateById(string id);
        Task<MongoTemplateEntity> Add(MongoTemplateEntity template);
        Task<MongoTemplateEntity> Update(MongoTemplateEntity template);
        Task Delete(string id);
    }
}