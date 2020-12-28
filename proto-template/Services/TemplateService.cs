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

        public override async Task<Template> Get(IdTemplate request, ServerCallContext context)
        {
            var response = await GetTemplateByIdIfPresent(request.Id);
            try
            {
                return _mapper.Map<TemplateGRPC.Template>(response);
            }
            catch (AutoMapperMappingException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Something went wrong with mapping to GRPC model."));
            }
        }

        public override async Task<Template> Add(TemplateAddRequest request, ServerCallContext context)
        {
            // TODO: Validation of the entity (is this required???)
            if (String.IsNullOrWhiteSpace(request.Name))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Name is required"));
            }

            try
            {
                var entity = _mapper.Map<TemplateEntity>(request);
                var response = await _repository.Add(entity);
                return _mapper.Map<TemplateGRPC.Template>(response);
            }
            catch (AutoMapperMappingException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Not all required fields are filled in."));
            }
        }

        public override async Task<Template> Update(TemplateUpdateRequest request, ServerCallContext context)
        {
            await GetTemplateByIdIfPresent(request.Id);
            try
            {
                var entity = _mapper.Map<TemplateEntity>(request);
                var response = await _repository.Update(entity);
                return _mapper.Map<TemplateGRPC.Template>(response);
            }
            catch (AutoMapperMappingException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Not all required fields are filled in."));
            }
        }

        public override async Task<TemplateEmpty> Delete(IdTemplate request, ServerCallContext context)
        {
            // check if the template exists first
            await GetTemplateByIdIfPresent(request.Id);
            await _repository.Delete(request.Id);

            return new TemplateEmpty { };
        }

        #region private generic methods

        private async Task<TemplateEntity> GetTemplateByIdIfPresent(string id)
        {
            var foundTemplate = await _repository.GetTemplateById(id);
            if (foundTemplate == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Template not found"));

            return foundTemplate;
        }

        #endregion
    }
}