using AutoMapper;
using CarRental.DataAccess.Abstract.Common;
using CarRental.DataAccess.Abstract.Insurances;
using CarRental.DataAccess.Abstract.Somatons;
using CarRental.DataAccess.Abstract.Vehicles;
using CarRental.Domain.Entities.Insurances;
using CarRental.Domain.Entities.Somatons;
using CarRental.Domain.Entities.Vehicles;
using CarRental.grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CarRental.Services.Services
{
    public class CarService : CarRental.grpc.Car.CarBase
    {
        private IVehicleRepository _carRepository;
        private IMapper _mapper;

        public CarService(IVehicleRepository repository, IMapper mapper)
        {
            _carRepository = repository;
            _mapper = mapper;
        }

        public override Task<CarDTO> CreateCar(CreateCarRequest request, ServerCallContext context)
        {
            DateTime fabricationDate = DateTime.Now;

            _carRepository.BeginTransaction();
            Insurance? insurance = ((IInsuranceRepository)_carRepository).GetInsurance(request.InsuranceId);
            Somaton? somaton = ((ISomatonRepository)_carRepository).GetSomaton(request.SomatonId);
            Domain.Entities.Common.Price? price = ((IPriceRepository)_carRepository).GetPrice(request.PriceId);

            var car = _carRepository.CreateCar(request.BrandName, fabricationDate, insurance, somaton);
            car.Price = price;
            car.CirculationId = request.CirculationId;
            car.SomatonId = request.SomatonId;
            car.PriceId = request.PriceId;
            _carRepository.CommitTransaction();
            return Task.FromResult(_mapper.Map<CarDTO>(car));
        }

        public override Task<NullableCarDTO> GetCar(GetRequest request, ServerCallContext context)
        {
            _carRepository.BeginTransaction();
            var car = _carRepository.GetVehicle<CarRental.Domain.Entities.Vehicles.Car>(request.Id);
            _carRepository.CommitTransaction();

            var result = new NullableCarDTO();
            if (car is not null)
                result.Car = _mapper.Map<CarDTO>(car);

            return Task.FromResult(result);
        }

        public override Task<Empty> UpdateCar(CarDTO request, ServerCallContext context)
        {
            var modifiedCar = _mapper.Map<CarRental.Domain.Entities.Vehicles.Car>(request);
            _carRepository.BeginTransaction();
            _carRepository.UpdateVehicle(modifiedCar);
            _carRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteCar(CarDTO request, ServerCallContext context)
        {
            var car = _mapper.Map<CarRental.Domain.Entities.Vehicles.Car>(request);
            _carRepository.BeginTransaction();
            _carRepository.DeleteVehicle(car);
            _carRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }
    }
}