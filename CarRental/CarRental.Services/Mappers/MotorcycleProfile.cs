using AutoMapper;
using CarRental.grpc;
using System.Drawing;

namespace CarRental.Services.Mappers
{
    public class MotorcycleProfile : Profile
    {
        public MotorcycleProfile()
        {
            CreateMap<CarRental.Domain.Entities.Vehicles.Motorcycle, MotorcycleDTO>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
                .ForMember(t => t.CirculationId, o => o.MapFrom(s => s.CirculationId))
                .ForMember(t => t.Color, o => o.MapFrom(s => s.Color.ToArgb()))
                .ForMember(t => t.Color2, o => o.MapFrom(s => s.Color2.ToArgb()))
                .ForMember(t => t.InsuranceId, o => o.MapFrom(s => s.InsuranceId))
                .ForMember(t => t.SomatonId, o => o.MapFrom(s => s.SomatonId))
                .ForMember(t => t.PriceId, o => o.MapFrom(s => s.PriceId))
            .ForMember(t => t.HasSideCar, o => o.MapFrom(s => s.HasSideCar));

            CreateMap<MotorcycleDTO, CarRental.Domain.Entities.Vehicles.Motorcycle>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
                .ForMember(t => t.CirculationId, o => o.MapFrom(s => s.CirculationId))
                .ForMember(t => t.Color, o => o.MapFrom(s => Color.FromArgb(s.Color)))
                .ForMember(t => t.Color2, o => o.MapFrom(s => Color.FromArgb(s.Color2)))
                .ForMember(t => t.InsuranceId, o => o.MapFrom(s => s.InsuranceId))
                .ForMember(t => t.SomatonId, o => o.MapFrom(s => s.SomatonId))
                .ForMember(t => t.PriceId, o => o.MapFrom(s => s.PriceId))
            .ForMember(t => t.HasSideCar, o => o.MapFrom(s => s.HasSideCar));
        }
    }
}