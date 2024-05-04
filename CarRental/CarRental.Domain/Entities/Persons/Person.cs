using CarRental.Domain.Abstract;
using CarRental.Domain.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities.Persons
{
    /// <summary>
    /// Clase base persona.
    /// </summary>
    public abstract class Person : Entity, ICountry
    {
        /// <summary>
        /// Nombre de la persona.
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// Apellido de la persona
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Edad de la persona.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Identificador de la persona.
        /// </summary>
        public string CI { get; set; }

        public string CountryName { get; set; }

        /// <summary>
        /// Correo electronico de la persona.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Numero de telefono de la persona.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Constructor requerido por EntityFramework
        /// </summary>
        protected Person()
        { }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="ci"></param>
        public Person(string name, string lastName, string ci)
        {
            Name = name;
            LastName = lastName;
            CI = ci;
        }
    }
}