﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Domain.Entities.Persons;
using CarRental.Domain.Entities.Types;
using CarRental.DataAccess.Abstract.Persons;

namespace CarRental.DataAccess.Repositories
{
    /// <summary>
    /// Define las operaciones en BD para personas
    /// </summary>
    public partial class ApplicationRepository : IPersonRepository
    {
        public Users CreateUser(string name, string lastName, string iD, UserRoleType role)
        {
            Users user = new(name, lastName, iD, role);
            _context.Add(user); return user;
        }

        public Client CreateClient(string name, string lastName, string iD)
        {
            Client client = new(name, lastName, iD);
            _context.Add(client);
            return client;
        }

        public P? GetPerson<P>(int id) where P : Person
        {
            return _context.Set<P>().Find(id);
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _context.Set<Person>().ToList();
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Set<Users>().ToList();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _context.Set<Client>().ToList();
        }

        public void UpdatePerson(Person person)
        {
            _context.Set<Person>().Update(person);
        }

        public void DeletePerson(Person person)
        {
            _context.Remove(person);
        }
    }
}