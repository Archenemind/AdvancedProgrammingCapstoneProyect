using AutoMapper;
using CarRental.grpc;

namespace CarRental.Services.Mappers
{
    public class CirculationProfile : Profile
    {
        public CirculationProfile()
        {
            CreateMap<CarRental.Domain.Entities.Circulations.Circulation, CirculationDTO>()
            .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
            .ForMember(t => t.Model, o => o.MapFrom(s => s.Model))
            .ForMember(t => t.Plate, o => o.MapFrom(s => s.Plate))
            .ForMember(t => t.MotorNumber, o => o.MapFrom(s => s.MotorNumber))
            .ForMember(t => t.InsuranceId, o => o.MapFrom(s => s.InsuranceID))
            .ForMember(t => t.SomatonId, o => o.MapFrom(s => s.SomatonId));

            CreateMap<CirculationDTO, CarRental.Domain.Entities.Circulations.Circulation>()
            .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
            .ForMember(t => t.Model, o => o.MapFrom(s => s.Model))
            .ForMember(t => t.Plate, o => o.MapFrom(s => s.Plate))
            .ForMember(t => t.MotorNumber, o => o.MapFrom(s => s.MotorNumber))
            .ForMember(t => t.InsuranceID, o => o.MapFrom(s => s.InsuranceId))
            .ForMember(t => t.SomatonId, o => o.MapFrom(s => s.SomatonId));
        }
            
    }
}