using AutoMapper;
using CarRental.grpc;

namespace CarRental.Services.Mappers
{
    public class SomatonProfile : Profile
    {
        public SomatonProfile()
        {
            CreateMap<CarRental.Domain.Entities.Somatons.Somaton, SomatonDTO>()
            .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
            .ForMember(t => t.ExpeditionDate, o => o.MapFrom(s => s.ExpeditionDate))
            .ForMember(t => t.Status, o => o.MapFrom(s => s.Status))
            .ForMember(t => t.Number, o => o.MapFrom(s => s.Number));


            CreateMap<SomatonDTO, CarRental.Domain.Entities.Somatons.Somaton>()
            .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
            .ForMember(t => t.ExpeditionDate, o => o.MapFrom(s => s.ExpeditionDate))
            .ForMember(t => t.Status, o => o.MapFrom(s => s.Status))
            .ForMember(t => t.Number, o => o.MapFrom(s => s.Number));
        }
    }
}