using AutoMapper;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.Abstract.Somatons;
using CarRental.Domain.Entities.Somatons;
using CarRental.Domain.Entities.Vehicles;
using CarRental.grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CarRental.Services.Services
{
    public class SomatonService : CarRental.grpc.Somaton.SomatonBase
    {
        private ISomatonRepository _somatonRepository;
        private IMapper _mapper;

        public SomatonService(ISomatonRepository repository, IMapper mapper)
        {
            _somatonRepository = repository;
            _mapper = mapper;
        }
        public override Task<SomatonDTO> CreateSomaton(CreateSomatonRequest request, ServerCallContext context)
        {
            DateTime expeditionDate = DateTime.Parse(request.ExpeditionDate);
            DateTime expeditionDateReal = DateTime.Now;
            _somatonRepository.BeginTransaction();
            var somaton = _somatonRepository.CreateSomaton(expeditionDateReal, (CarRental.Domain.Entities.Types.Status)request.Status, request.Number);
            _somatonRepository.CommitTransaction();

            return Task.FromResult(_mapper.Map<SomatonDTO>(somaton));
        }
        public override Task<NullableSomatonDTO> GetSomaton(GetRequest request, ServerCallContext context)
        {
            _somatonRepository.BeginTransaction();
            var somaton = _somatonRepository.GetSomaton(request.Id);
            _somatonRepository.CommitTransaction();

            var result = new NullableSomatonDTO();
            if (somaton != null)
            {
                result.Somaton = _mapper.Map<SomatonDTO>(somaton);
            }
            return Task.FromResult(result);
        }
        public override Task<Empty> UpdateSomaton(SomatonDTO request, ServerCallContext context)
        {
            var modifyedSomaton = _mapper.Map<CarRental.Domain.Entities.Somatons.Somaton>(request);
            _somatonRepository.BeginTransaction();
            _somatonRepository.UpdateSomaton(modifyedSomaton);
            _somatonRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }
        public override Task<Empty> DeleteSomaton(SomatonDTO request, ServerCallContext context)
        {
            var somaton = _mapper.Map<CarRental.Domain.Entities.Somatons.Somaton>(request);
            _somatonRepository.BeginTransaction();
            _somatonRepository.DeleteSomaton(somaton);  
            _somatonRepository.CommitTransaction(); 

            return Task.FromResult(new Empty());
        }
    }
}