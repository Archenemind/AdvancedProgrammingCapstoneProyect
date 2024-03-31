using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Domain.Entities.Insurances;
using static CarRental.Domain.Entities.Insurances.Insurance;
using CarRental.DataAccess.Abstract.Insurances;
using CarRental.Domain.Entities.Circulations;

namespace CarRental.DataAccess.Repositories
{
    /// <summary>
    /// Clase parcial referida a los seguros
    /// </summary>
    public partial class ApplicationRepository : IInsuranceRepository
    {
        public Insurance CreateInsurance(string policyNumber)

        {
            Insurance insurance = new Insurance(policyNumber);
            _context.Add(insurance); return insurance;
        }

        public Insurance? GetInsurance(int id)
        {
            return _context.Set<Insurance>().Find(id);
        }

        public void UpdateInsurance(Insurance insurance)
        {
            _context.Set<Insurance>().Update(insurance);
        }

        public void DeleteInsurance(Insurance insurance)
        {
            _context.Remove(insurance);
        }
    }
}