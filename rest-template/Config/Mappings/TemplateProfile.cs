using AutoMapper;
using Back_End.Persistence.Entities;
using Back_End.Persistence.Models;
using MongoDB.Bson;

namespace Back_End.Config.Mappings
{
    public class TemplateProfile : Profile
    {
        public TemplateProfile()
        {
            CreateMap<TemplateDTO, TemplateEntity>();
            CreateMap<TemplateEntity, TemplateDTO>();
            CreateMap<object, BsonDocument>().ConvertUsing(obj => BsonDocument.Parse(obj.ToString()));
        }
    }
}