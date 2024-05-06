using AutoMapper;
using CarRental.grpc;

namespace CarRental.Services.Mappers
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            CreateMap<CarRental.Domain.Entities.Insurances.Insurance, InsuranceDTO>()
            .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
            .ForMember(t => t.PolicyNumber, o => o.MapFrom(s => s.PolicyNumber));

            CreateMap<InsuranceDTO, CarRental.Domain.Entities.Insurances.Insurance>()
           .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
           .ForMember(t => t.PolicyNumber, o => o.MapFrom(s => s.PolicyNumber));
        }
    }
}