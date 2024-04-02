using System;
using System.ComponentModel.DataAnnotations.Schema;
using CarRental.Domain.Entities.Common;
using CarRental.Domain.Entities.Reservations;

namespace CarRental.Domain.Entities.Supplements
{
    /// <summary>
    /// Modela el suplemento de un auto
    /// </summary>
    public class Supplement : Entity
    {
        /// <summary>
        /// Precio del suplemento
        /// </summary>
        public Price Price { get; set; }

        /// <summary>
        /// Reservacion del suplemento
        /// </summary>
        [NotMapped]
        public Reservation Reservation { get; set; }

        /// <summary>
        /// Identificador de la reservacion
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        ///Descripcion del suplemento
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor requerido por  EntityFrameworkCore para migraciones
        /// </summary>
        protected Supplement()
        { }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="price"></param>
        public Supplement(Price price, string description)
        {
            Price = price;
            Description = description;
        }
    }
}