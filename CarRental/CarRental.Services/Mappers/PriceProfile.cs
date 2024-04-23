using AutoMapper;
using CarRental.Domain;
using CarRental.grpc;

namespace CarRental.Services.Mappers
{
    public class PriceProfile : Profile
    {
        public PriceProfile()
        {

            CreateMap<CarRental.Domain.Entities.Common.Price, PriceDTO>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
                .ForMember(t => t.MoneyType, o => o.MapFrom(s => s.Currency))
                .ForMember(t => t.Value, o => o.MapFrom(s => s.Value));

            CreateMap<PriceDTO, CarRental.Domain.Entities.Common.Price>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id))
                .ForMember(t => t.Currency, o => o.MapFrom(s => s.MoneyType))
                .ForMember(t => t.Value, o => o.MapFrom(s => s.Value));

        }

    }
}