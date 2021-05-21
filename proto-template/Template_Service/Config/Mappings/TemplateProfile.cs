using AutoMapper;
using Template_Service.Persistence.Entities;
using Template_Service.Proto;

namespace Template_Service.Config.Mappings {
    public class TemplateProfile : Profile {
        public TemplateProfile() {
            CreateMap<Template, MongoTemplateEntity>();
            CreateMap<MongoTemplateEntity, Template>();

            CreateMap<TemplateUpdateRequest, MongoTemplateEntity>();
            CreateMap<MongoTemplateEntity, TemplateUpdateRequest>();
        }
    }
}