using AutoMapper;
using CarRental.DataAccess.Abstract.Persons;
using CarRental.DataAccess.Abstract.Reservations;
using CarRental.DataAccess.Abstract.Vehicles;
using CarRental.Domain.Entities.Types;
using CarRental.Domain.Entities.Vehicles;
using CarRental.grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CarRental.Services.Services
{
    public class ReservationService : Reservation.ReservationBase
    {
        private IReservationRepository _reservationRepository;
        private IMapper _mapper;

        public ReservationService(IReservationRepository repository, IMapper mapper)
        {
            _reservationRepository = repository;
            _mapper = mapper;
        }

        public override Task<ReservationDTO> CreateReservation(CreateReservationRequest request, ServerCallContext context)
        {
            _reservationRepository.BeginTransaction();

            CarRental.Domain.Entities.Persons.Client? client = ((IPersonRepository)_reservationRepository).GetPerson<Domain.Entities.Persons.Client>(request.ClientId);
            Vehicle? vehicle = ((IVehicleRepository)_reservationRepository).GetVehicle<Domain.Entities.Vehicles.Car>(1);

            var reservation = _reservationRepository.CreateReservation(client, vehicle);
            _reservationRepository.CommitTransaction();
            return Task.FromResult(_mapper.Map<ReservationDTO>(reservation));
        }

        public override Task<NullableReservationDTO> GetReservation(GetRequest request, ServerCallContext context)
        {
            _reservationRepository.BeginTransaction();
            var reservation = _reservationRepository.GetReservation(request.Id);
            _reservationRepository.CommitTransaction();

            var result = new NullableReservationDTO();
            if (reservation is not null)
                result.Reservation = _mapper.Map<ReservationDTO>(reservation);

            return Task.FromResult(result);
        }

        public override Task<Empty> UpdateReservation(ReservationDTO request, ServerCallContext context)
        {
            var modifiedReservation = _mapper.Map<CarRental.Domain.Entities.Reservations.Reservation>(request);
            _reservationRepository.BeginTransaction();
            _reservationRepository.UpdateReservation(modifiedReservation);
            _reservationRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteReservation(ReservationDTO request, ServerCallContext context)
        {
            var reservation = _mapper.Map<CarRental.Domain.Entities.Reservations.Reservation>(request);
            _reservationRepository.BeginTransaction();
            _reservationRepository.DeleteReservation(reservation);
            _reservationRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }
    }
}