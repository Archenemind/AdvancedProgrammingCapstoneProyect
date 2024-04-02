using CarRental.Domain.Entities.Persons;
using CarRental.Domain.Entities.Common;

using CarRental.Domain.Entities.Types;
using CarRental.Domain.Entities.Vehicles;

using CarRental.Domain.Entities.Supplements;

using CarRental.Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;
using CarRental.Domain.Utilities.Converters;

namespace CarRental.Domain.Entities.Reservations
{
    /// <summary>
    /// Modela la reservacion de un vehiculo.
    /// </summary>
    public class Reservation : Entity
    {
        #region Properties

        /// <summary>
        /// Cliente que realizó la renta.
        /// </summary>
        [NotMapped]
        public Client Client { get; }

        /// <summary>
        /// Identificador unico del cliente
        /// </summary>
        private int ClientId { get; set; }

        /// <summary>
        /// Vehículo a rentar.
        /// </summary>
        [NotMapped]
        public Vehicle Vehicle { get; }

        /// <summary>
        /// Identificador unico del vehiculo
        /// </summary>
        private int VehicleId { get; set; }

        /// <summary>
        /// Fecha de inicio de la renta.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Fecha de finalizada la renta.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Estado de la reservacion
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Suplementos que se le añadiran al auto rentado
        /// </summary>
        [NotMapped]
        public ICollection<Supplement> Supplements { get; set; }

        public void GetPrice()
        {
            PriceConverter priceConverter = new();
            if (Supplements.Count != 0)
            {
                double supplementsValue = 0;
                Supplement firstSupplement = Supplements.OfType<Supplement>().FirstOrDefault();
                MoneyType moneyType = firstSupplement.Price.Currency;
                foreach (Supplement sup in Supplements)
                {
                    Price supPrice = priceConverter.ConvertTo(moneyType, sup.Price);

                    supplementsValue += supPrice.Value;
                }

                Price vehiclePrice = priceConverter.ConvertTo(moneyType, Vehicle.Price);
                Console.WriteLine($"The price for the reservation is {supplementsValue + vehiclePrice.Value} {vehiclePrice.Currency}");
            }
            else
            {
                Console.WriteLine($"The price for the reservation is {Vehicle.Price.Value} {Vehicle.Price.Currency}");
            }
        }

        /// <summary>
        /// Constructor requerido por EntityFrameworkCore para migraciones
        /// </summary>
        protected Reservation()
        { }

        /// <summary>
        /// Inicializa una reservacion
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vehicle"></param>
        public Reservation(Client client, Vehicle vehicle)
        {
            Client = client;
            Vehicle = vehicle;
            Status = Status.Requested;
            StartDate = DateTime.Now;
        }

        #endregion Properties
    }
}