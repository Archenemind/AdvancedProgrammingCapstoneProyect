using CarRental.Domain.Entities.Common;
using CarRental.Domain.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Entities.Persons
{
    /// <summary>
    /// Modela un cliente de la renta de autos.
    /// </summary>
    public class Client : Person
    {
        /// <summary>
        /// Lista de las reservaciones del cliente.
        /// </summary>
        [NotMapped]
        public ICollection<Reservation> Reservations { get; set; }

        /// <summary>
        /// Constructor requerido por EntityFrameworkCore para migraciones
        /// </summary>
        protected Client()
        { }

        /// <summary>
        /// Inicializa un cliente
        /// </summary>
        /// <param name="name">Nombre</param>
        /// <param name="lastName">Apellido</param>
        /// <param name="ci">Carnet de identidad</param>
        public Client(string name, string lastName, string ci) : base(name, lastName, ci)
        {
        }
    }
}