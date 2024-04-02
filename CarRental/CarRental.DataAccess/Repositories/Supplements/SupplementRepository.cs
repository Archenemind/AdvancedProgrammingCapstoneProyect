using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Domain.Entities.Common;
using CarRental.Domain.Entities.Supplements;
using CarRental.DataAccess.Abstract.Supplements;
using CarRental.Domain.Entities.Circulations;

namespace CarRental.DataAccess.Repositories
{
    /// <summary>
    /// Clase parcial referida a los suplementos
    /// </summary>
    public partial class ApplicationRepository : ISupplementRepository
    {
        public Supplement CreateSupplement(Price price, string description)
        {
            Supplement supplement = new Supplement(price, description);
            _context.Add(supplement); return supplement;
        }

        public Supplement? GetSupplement(int id)
        {
            return _context.Set<Supplement>().Find(id);
        }

        public void UpdateSupplement(Supplement supplement
            )
        {
            _context.Set<Supplement>().Update(supplement);
        }

        public void DeleteSupplement(Supplement supplement)
        {
            _context.Remove(supplement);
        }
    }
}