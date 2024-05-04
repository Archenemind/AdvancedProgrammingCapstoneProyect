using System;

using CarRental.Domain.Entities.Types;

namespace CarRental.Domain.Entities.Persons
{
    /// <summary>
    /// Modela la clase usuario que puede ser
    /// un admin, un empleado, un proveedor.
    /// </summary>
    public class Users : Person
    {
        /// <summary>
        /// Tipo de usuario
        /// </summary>
        public UserRoleType Role { get; set; }

        /// <summary>
        /// Constructor requerido por Entity Framework
        /// </summary>
        protected Users()
        { }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="name">Nombre</param>
        /// <param name="lastName">Apellido</param>
        /// <param name="ci">Identificador</param>
        /// <param name="role">Rol</param>
        public Users(string name, string lastName, string ci, UserRoleType role) : base(name, lastName, ci)
        {
            Role = role;
        }
    }
}