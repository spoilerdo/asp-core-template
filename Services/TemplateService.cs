using System;
using System.Threading.Tasks;
using AutoMapper;
using Back_End.Persistence.Entities;
using Back_End.Persistence.Repositories;
using Grpc.Core;
using TemplateGRPC;

namespace Back_End.Services
{
    public class TemplateService : Template_Service.Template_ServiceBase
    {
        private readonly ITemplateRepository _repository;
        private readonly IMapper _mapper;

        public TemplateService(ITemplateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<Template> Add(TemplateAddRequest request, ServerCallContext context)
        {
            // TODO: Validation of the entity (is this required???)
            if (String.IsNullOrWhiteSpace(request.Name))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Name is required"));
            }

            try
            {
                TemplateEntity entity = _mapper.Map<TemplateEntity>(request);
            }
            catch (AutoMapperMappingException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Not all required fields are filled in."));
            }

            return base.Add(request, context);
        }

        public override Task<TemplateEmpty> Delete(IdTemplate request, ServerCallContext context)
        {
            return base.Delete(request, context);
        }

        public override Task<Template> Get(IdTemplate request, ServerCallContext context)
        {
            return base.Get(request, context);
        }

        public override Task<Template> Update(TemplateUpdateRequest request, ServerCallContext context)
        {
            return base.Update(request, context);
        }
    }
}