﻿using CarRental.Domain.Entities.Common;
using CarRental.Domain.Entities.Insurances;
using CarRental.Domain.Entities.Somatons;
using CarRental.Domain.Entities.Types;
using System.Drawing;

namespace CarRental.Domain.Entities.Vehicles
{
    /// <summary>
    /// Esta clase modela un carro
    /// </summary>
    public class Car : Vehicle
    {
        /// <summary>
        /// Si tiene aire acondicionado
        /// </summary>
        public bool HasAirConditioning { get; set; }

        /// <summary>
        /// Numero de velocidades
        /// </summary>
        public int NumberOfVelocities { get; }

        /// <summary>
        /// Maxima velocidad garantizada por el fabricante
        /// </summary>
        public int MaxVelocity { get; }

        /// <summary>
        /// Constructor requerido por EntityFrameworkCore para migraciones
        /// </summary>
        protected Car()
        { }

        /// <summary>
        /// Inicializa un carro
        /// </summary>
        /// <param name="numberOfVelocities"></param>
        /// <param name="maxVelocity"></param>
        /// <param name="hasAirConditioning"></param>
        public Car(string brandName, DateTime fabricationDate, Insurance insurance, Somaton somaton) : base(brandName, fabricationDate, insurance, somaton)
        {
        }
    }
}