using System;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Template_Service.Proto;
using Template_Service.Persistence.Entities;
using Template_Service.Persistence.Repositories;
using Google.Protobuf.WellKnownTypes;

namespace Template_Service.Services
{
    public class TemplateService : ITemplateService.ITemplateServiceBase
    {
        private readonly IMongoTemplateRepository _repository;
        private readonly IMapper _mapper;

        public TemplateService(IMongoTemplateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Template> Get(IdTemplate request, ServerCallContext context)
        {
            var response = await GetTemplateByIdIfPresent(request.Id);
            try
            {
                return _mapper.Map<Template>(response);
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
                var entity = _mapper.Map<MongoTemplateEntity>(request);
                var response = await _repository.Add(entity);
                return _mapper.Map<Template>(response);
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
                var entity = _mapper.Map<MongoTemplateEntity>(request);
                var response = await _repository.Update(entity);
                return _mapper.Map<Template>(response);
            }
            catch (AutoMapperMappingException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Not all required fields are filled in."));
            }
        }

        public override async Task<Empty> Delete(IdTemplate request, ServerCallContext context)
        {
            // check if the template exists first
            await GetTemplateByIdIfPresent(request.Id);
            await _repository.Delete(request.Id);

            return new Empty { };
        }

        #region private generic methods

        private async Task<MongoTemplateEntity> GetTemplateByIdIfPresent(string id)
        {
            var foundTemplate = await _repository.GetTemplateById(id);
            if (foundTemplate == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Template not found"));

            return foundTemplate;
        }

        #endregion
    }
}