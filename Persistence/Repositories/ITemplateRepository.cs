using System.Threading.Tasks;
using Back_End.Persistence.Common;
using Back_End.Persistence.Entities;

namespace Back_End.Persistence.Repositories
{
    public interface ITemplateRepository
    {
        Task<DataResponseObject<TemplateEntity>> Add(TemplateEntity template);
        Task<DataResponseObject<TemplateEntity>> GetTemplateById(string id);
    }
}