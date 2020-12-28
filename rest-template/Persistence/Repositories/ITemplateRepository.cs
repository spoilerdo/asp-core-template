using System.Threading.Tasks;
using Back_End.Persistence.Entities;

namespace Back_End.Persistence.Repositories
{
    public interface ITemplateRepository
    {
        Task<TemplateEntity> GetTemplateById(string id);
        Task<TemplateEntity> Add(TemplateEntity template);
        Task<TemplateEntity> Update(TemplateEntity template);
        Task Delete(string id);
    }
}