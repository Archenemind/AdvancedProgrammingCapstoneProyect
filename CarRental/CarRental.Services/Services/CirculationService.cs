using AutoMapper;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.Abstract.Circulations;
using CarRental.DataAccess.Abstract.Insurances;
using CarRental.DataAccess.Abstract.Somatons;
using CarRental.Domain.Entities.Vehicles;
using CarRental.grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CarRental.Services.Services
{
    public class CirculationService : CarRental.grpc.Circulation.CirculationBase
    {
        private ICirculationRepository _circulationRepository;
        private IMapper _mapper;

        public CirculationService(ICirculationRepository repository, IMapper mapper)
        {
           _circulationRepository = repository;
            _mapper = mapper;
        }
        public override Task<CirculationDTO> CreateCirculation(CreateCirculationRequest request, ServerCallContext context)
        {
            _circulationRepository.BeginTransaction();
            CarRental.Domain.Entities.Insurances.Insurance? insurance = ((IInsuranceRepository)_circulationRepository).GetInsurance(request.InsuranceId);
            CarRental.Domain.Entities.Somatons.Somaton? somaton = ((ISomatonRepository)_circulationRepository).GetSomaton(request.SomatonId);


            var circulation = _circulationRepository.CreateCirculation(request.Model, request.Plate, request.MotorNumber, insurance, somaton);
            circulation.InsuranceID = request.InsuranceId;
            circulation.SomatonId = request.SomatonId;
            _circulationRepository.CommitTransaction();

            return Task.FromResult(_mapper.Map<CirculationDTO>(circulation));
        }
        public override Task<NullableCirculationDTO> GetCirculation(GetRequest request, ServerCallContext context)
        {
            _circulationRepository.BeginTransaction();
            var circulation = _circulationRepository.GetCirculation(request.Id);
            _circulationRepository.CommitTransaction();

            var result = new NullableCirculationDTO();
            if(circulation != null)
            {
                result.Circulation = _mapper.Map <CirculationDTO>(circulation);
            }

            return Task.FromResult(result);
        }
        public override Task<Empty> UpdateCirculation(CirculationDTO request, ServerCallContext context)
        {
            var modifyedCirculation = _mapper.Map<CarRental.Domain.Entities.Circulations.Circulation>(request);
            _circulationRepository.BeginTransaction();
            _circulationRepository.UpdateCirculation(modifyedCirculation);
            _circulationRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }
        public override Task<Empty> DeleteCirculation(CirculationDTO request, ServerCallContext context)
        {
            var circulation = _mapper.Map<CarRental.Domain.Entities.Circulations.Circulation>(request);
            _circulationRepository.BeginTransaction();
            _circulationRepository.DeleteCirculation(circulation);
            _circulationRepository.CommitTransaction(); 

            return Task.FromResult(new Empty());
        }
    }
}