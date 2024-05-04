using AutoMapper;
using CarRental.DataAccess.Abstract.Common;
using CarRental.DataAccess.Abstract.Insurances;
using CarRental.DataAccess.Abstract.Somatons;
using CarRental.DataAccess.Abstract.Vehicles;
using CarRental.Domain.Entities.Insurances;
using CarRental.Domain.Entities.Somatons;
using CarRental.grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CarRental.Services.Services
{
    public class MotorcycleService : CarRental.grpc.Motorcycle.MotorcycleBase
    {
        private IVehicleRepository _motorcycleRepository;
        private IMapper _mapper;

        public MotorcycleService(IVehicleRepository repository, IMapper mapper)
        {
            _motorcycleRepository = repository;
            _mapper = mapper;
        }

        public override Task<MotorcycleDTO> CreateMotorcycle(CreateMotorcycleRequest request, ServerCallContext context)
        {
            DateTime fabricationDate = DateTime.Now;

            _motorcycleRepository.BeginTransaction();
            Insurance? insurance = ((IInsuranceRepository)_motorcycleRepository).GetInsurance(request.InsuranceId);
            Somaton? somaton = ((ISomatonRepository)_motorcycleRepository).GetSomaton(request.SomatonId);
            Domain.Entities.Common.Price? price = ((IPriceRepository)_motorcycleRepository).GetPrice(request.PriceId);

            var motorcycle = _motorcycleRepository.CreateMotorcycle(request.BrandName, fabricationDate, insurance, somaton);
            motorcycle.Price = price;
            motorcycle.CirculationId = request.CirculationId;
            motorcycle.SomatonId = request.SomatonId;
            motorcycle.PriceId = request.PriceId;
            _motorcycleRepository.CommitTransaction();
            return Task.FromResult(_mapper.Map<MotorcycleDTO>(motorcycle));
        }

        public override Task<NullableMotorcycleDTO> GetMotorcycle(GetRequest request, ServerCallContext context)
        {
            _motorcycleRepository.BeginTransaction();
            var motorcycle = _motorcycleRepository.GetVehicle<CarRental.Domain.Entities.Vehicles.Motorcycle>(request.Id);
            _motorcycleRepository.CommitTransaction();

            var result = new NullableMotorcycleDTO();
            if (motorcycle is not null)
                result.Motorcycle = _mapper.Map<MotorcycleDTO>(motorcycle);

            return Task.FromResult(result);
        }

        public override Task<Empty> UpdateMotorcycle(MotorcycleDTO request, ServerCallContext context)
        {
            var modifiedMotorcycle = _mapper.Map<CarRental.Domain.Entities.Vehicles.Motorcycle>(request);
            _motorcycleRepository.BeginTransaction();
            _motorcycleRepository.UpdateVehicle(modifiedMotorcycle);
            _motorcycleRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteMotorcycle(MotorcycleDTO request, ServerCallContext context)
        {
            var motorcycle = _mapper.Map<CarRental.Domain.Entities.Vehicles.Motorcycle>(request);
            _motorcycleRepository.BeginTransaction();
            _motorcycleRepository.DeleteVehicle(motorcycle);
            _motorcycleRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }
    }
}