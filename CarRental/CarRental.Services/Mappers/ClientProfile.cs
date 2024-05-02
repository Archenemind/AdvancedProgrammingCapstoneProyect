using AutoMapper;
using CarRental.grpc;

using CarRental.Domain;

namespace CarRental.Services.Mappers
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CarRental.Domain.Entities.Persons.Client, ClientDTO>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(t => t.CI, o => o.MapFrom(s => s.CI))
                .ForMember(t => t.CountryName, o => o.MapFrom(s => s.CountryName))
                .ForMember(t => t.Phone, o => o.MapFrom(s => s.Phone));

            CreateMap<ClientDTO, CarRental.Domain.Entities.Persons.Client>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(t => t.CI, o => o.MapFrom(s => s.CI))
                .ForMember(t => t.CountryName, o => o.MapFrom(s => s.CountryName))
                .ForMember(t => t.Phone, o => o.MapFrom(s => s.Phone));
        }
    }
}