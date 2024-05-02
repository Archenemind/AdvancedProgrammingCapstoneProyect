using AutoMapper;
using CarRental.DataAccess.Abstract.Common;
using CarRental.Domain.Entities.Types;
using CarRental.grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CarRental.Services.Services
{
    public class PriceService : Price.PriceBase
    {
        private IPriceRepository _priceRepository;
        private IMapper _mapper;

        public PriceService(IPriceRepository repository, IMapper mapper)
        {
            _priceRepository = repository;
            _mapper = mapper;
        }

        public override Task<PriceDTO> CreatePrice(CreatePriceRequest request, ServerCallContext context)
        {
            _priceRepository.BeginTransaction();
            var price = _priceRepository.CreatePrice((MoneyType)request.MoneyType, request.Value);
            _priceRepository.CommitTransaction();
            return Task.FromResult(_mapper.Map<PriceDTO>(price));
        }

        public override Task<NullablePriceDTO> GetPrice(GetRequest request, ServerCallContext context)
        {
            _priceRepository.BeginTransaction();
            var price = _priceRepository.GetPrice(request.Id);
            _priceRepository.CommitTransaction();

            var result = new NullablePriceDTO();
            if (price is not null)
                result.Price = _mapper.Map<PriceDTO>(price);

            return Task.FromResult(result);
        }

        public override Task<Empty> UpdatePrice(PriceDTO request, ServerCallContext context)
        {
            var modifiedPrice = _mapper.Map<CarRental.Domain.Entities.Common.Price>(request);
            _priceRepository.BeginTransaction();
            _priceRepository.UpdatePrice(modifiedPrice);
            _priceRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeletePrice(PriceDTO request, ServerCallContext context)
        {
            var price = _mapper.Map<CarRental.Domain.Entities.Common.Price>(request);
            _priceRepository.BeginTransaction();
            _priceRepository.DeletePrice(price);
            _priceRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }
    }
}