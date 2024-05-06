using AutoMapper;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.Abstract.Insurances;
using CarRental.grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CarRental.Services.Services
{
    public class InsuranceService : CarRental.grpc.Insurance.InsuranceBase
    {
        private IInsuranceRepository _insuranceRepository;
        private IMapper _mapper;

        public InsuranceService(IInsuranceRepository repository, IMapper mapper)
        {
            _insuranceRepository = repository;
            _mapper = mapper;
        }
        public override Task<InsuranceDTO> CreateInsurance(CreateInsuranceRequest request, ServerCallContext context)
        {
            _insuranceRepository.BeginTransaction();
            var insurance = _insuranceRepository.CreateInsurance(request.PolicyNumber);
            _insuranceRepository.CommitTransaction();
            return Task.FromResult(_mapper.Map<InsuranceDTO>(insurance));
        }
        public override Task<NullableInsuranceDTO> GetInsurance(GetRequest request, ServerCallContext context)
        {
            _insuranceRepository.BeginTransaction();
            var insurance = _insuranceRepository.GetInsurance(request.Id);
            _insuranceRepository.CommitTransaction();

            var result = new NullableInsuranceDTO();
            if(insurance != null)
            {
                result.Insurance = _mapper.Map<InsuranceDTO>(insurance);
            }
            return Task.FromResult(result);
        }
        public override Task<Empty> UpdateInsurance(InsuranceDTO request, ServerCallContext context)
        {
            var modifyedInsurance = _mapper.Map<CarRental.Domain.Entities.Insurances.Insurance>(request);
            _insuranceRepository.BeginTransaction();
            _insuranceRepository.UpdateInsurance(modifyedInsurance);
            _insuranceRepository.CommitTransaction();

            return Task.FromResult(new Empty());
        }
        public override Task<Empty> DeleteInsurance(InsuranceDTO request, ServerCallContext context)
        {
            var insurance = _mapper.Map<CarRental.Domain.Entities.Insurances.Insurance>(request);
            _insuranceRepository.BeginTransaction();
            _insuranceRepository.DeleteInsurance(insurance);
            _insuranceRepository.CommitTransaction();
            return Task.FromResult(new Empty());
        }
    }
}