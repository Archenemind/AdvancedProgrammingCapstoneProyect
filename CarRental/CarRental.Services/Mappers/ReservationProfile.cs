using AutoMapper;
using CarRental.grpc;

namespace CarRental.Services.Mappers
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<CarRental.Domain.Entities.Reservations.Reservation, ReservationDTO>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
                .ForMember(t => t.ClientId, o => o.MapFrom(s => s.Client.Id))
                .ForMember(t => t.VehicleId, o => o.MapFrom(s => s.VehicleId))
                .ForMember(t => t.EndDate, o => o.MapFrom(s => s.EndDate))
                .ForMember(t => t.Status, o => o.MapFrom(s => s.Status));

            CreateMap<ReservationDTO, CarRental.Domain.Entities.Reservations.Reservation>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
                .ForMember(t => t.ClientId, o => o.MapFrom(s => s.ClientId))
                .ForMember(t => t.VehicleId, o => o.MapFrom(s => s.VehicleId))
                .ForMember(t => t.EndDate, o => o.MapFrom(s => s.EndDate))
                .ForMember(t => t.Status, o => o.MapFrom(s => s.Status));
            ;
        }
    }
}