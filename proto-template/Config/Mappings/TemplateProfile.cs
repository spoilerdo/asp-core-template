using AutoMapper;
using Back_End.Persistence.Entities;
using TemplateGRPC;

namespace Back_End.Config.Mappings
{
    public class TemplateProfile : Profile
    {
        public TemplateProfile()
        {
            CreateMap<Template, TemplateEntity>();
            CreateMap<TemplateEntity, Template>();

            CreateMap<TemplateUpdateRequest, TemplateEntity>();
            CreateMap<TemplateEntity, TemplateUpdateRequest>();
        }
    }
}