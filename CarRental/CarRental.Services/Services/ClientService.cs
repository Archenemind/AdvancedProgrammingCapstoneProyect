using AutoMapper;
using CarRental.DataAccess.Abstract.Common;
using CarRental.DataAccess.Abstract.Persons;
using CarRental.Domain.Entities.Persons;
using CarRental.Domain.Entities.Types;
using CarRental.grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CarRental.Services.Services
{
    public class ClientService : CarRental.grpc.Client.ClientBase
    {
        private IPersonRepository _clientRepository;
        private IMapper _mapper;

        public ClientService(IPersonRepository repository, IMapper mapper)
        {
            _clientRepository = repository;
            _mapper = mapper;
        }

        public override Task<ClientDTO> CreateClient(CreateClientRequest request, ServerCallContext context)
        {
            _clientRepository.BeginTransaction();
            var client = _clientRepository.CreateClient(request.Name, request.LastName, request.CI);
            client.CountryName = request.CountryName;
            client.Phone = request.Phone;
            _clientRepository.CommitTransaction();
            return Task.FromResult(_mapper.Map<ClientDTO>(client));
        }

        public override Task<NullableClientDTO> GetClient(GetRequest request, ServerCallContext context)
        {
            _clientRepository.BeginTransaction();
            var client = _clientRepository.GetPerson<CarRental.Domain.Entities.Persons.Client>(request.Id);
            _clientRepository.CommitTransaction();

            var result = new NullableClientDTO();
            if (client is not null)
                result.Client = _mapper.Map<ClientDTO>(client);

            return Task.FromResult(result);
        }

        public override Task<Empty> UpdateClient(ClientDTO request, ServerCallContext context)
        {
            var modifiedClient = _mapper.Map<CarRental.Domain.Entities.Persons.Client>(request);
            _clientRepository.BeginTransaction();
            _clientRepository.UpdatePerson(modifiedClient);
            _clientRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteClient(ClientDTO request, ServerCallContext context)
        {
            var client = _mapper.Map<CarRental.Domain.Entities.Persons.Client>(request);
            _clientRepository.BeginTransaction();
            _clientRepository.DeletePerson(client);
            _clientRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }
    }
}