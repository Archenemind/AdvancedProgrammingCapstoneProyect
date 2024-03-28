﻿using System;
using System.Drawing;
using CarRental.Domain.Abstract;
using CarRental.Domain.Entities.Somatons;
using CarRental.Domain.Entities.Insurances;
using CarRental.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities.Circulations
{
    /// <summary>
    /// Clase que modela una circulacion
    /// </summary>
    public class Circulation : Entity, ITransportable
    {
        public string BrandName { get; }

        public string Model { get; }

        public DateTime FabricationDate { get; }

        public string Plate { get; }

        public string MotorNumber { get; }

        public Color Color { get; set; }

        public Color Color2 { get; set; }

        [NotMapped]
        public Insurance Insurance { get; }

        /// <summary>
        /// Identificador unico de Insurance
        /// </summary>
        public int InsuranceID { get; set; }

        [NotMapped]
        public Somaton Somaton { get; }

        /// <summary>
        /// Identificador unico de Somaton
        /// </summary>
        public int SomatonId { get; set; }

        /// <summary>
        /// Fecha de Expiracion de la reservacion
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Fecha de creado
        /// </summary>
        public DateTime ExpeditionDate { get; set; }

        public string VIN { get; set; }

        /// <summary>
        /// Constructor requerido por Entity Framework
        /// </summary>
        protected Circulation()
        { }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="brandName"></param>
        /// <param name="model"></param>
        /// <param name="fabricationDate"></param>
        /// <param name="plate"></param>
        /// <param name="motorNumber"></param>
        /// <param name="color"></param>
        /// <param name="color2"></param>
        /// <param name="insurance"></param>
        /// <param name="insuranceID"></param>
        /// <param name="somaton"></param>
        /// <param name="somatonId"></param>
        /// <param name="expirationDate"></param>
        /// <param name="expeditionDate"></param>
        /// <param name="vin"></param>
        public Circulation(string model, string plate, string motorNumber, Insurance insurance, Somaton somaton)
        {
            Model = model;
            Plate = plate;
            MotorNumber = motorNumber;
            Insurance = insurance;
            Somaton = somaton;

        }
    }
}