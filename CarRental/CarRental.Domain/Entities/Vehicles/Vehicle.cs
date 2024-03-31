using CarRental.Domain.Abstract;
using CarRental.Domain.Entities.Common;
using CarRental.Domain.Entities.Types;
using CarRental.Domain.Entities.Somatons;
using CarRental.Domain.Entities.Insurances;
using CarRental.Domain.Entities.Circulations;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Entities.Vehicles
{
    /// <summary>
    /// Clase base para los vehículos del negocio
    /// </summary>
    public abstract class Vehicle : Entity, ITransportable
    {
        #region Properties

        /// <summary>
        /// Circulacion del vehiculo
        /// </summary>
        public Circulation Circulation { get; set; }

        public string BrandName { get; }

        public DateTime FabricationDate { get; }

        public string Plate { get; }

        public string MotorNumber { get; }

        public Color Color { get; set; }

        public Color Color2 { get; set; }

        public Insurance Insurance { get; }

        public Somaton Somaton { get; }

        public string Model { get; }

        /// <summary>
        /// Capacidad de pasajeros del vehiculo
        /// </summary>
        public int PassengerCapacity { get; }

        /// <summary>
        /// Precio del vehículo.
        /// </summary>
        public Price Price { get; set; }

        /// <summary>
        /// Peso del vehiculo
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// Constructor requerido por EntityFrameworkCore
        /// </summary>
        protected Vehicle()
        { }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        public Vehicle(string brandName, DateTime fabricationDate, Insurance insurance, Somaton somaton)
        {
            BrandName = brandName;
            FabricationDate = fabricationDate;
            Insurance = insurance;
            Somaton = somaton;
            Price = new(MoneyType.MN, 0);
            Color = Color.Empty;
            Color2 = Color.Empty;
        }

        #endregion Properties
    }
}