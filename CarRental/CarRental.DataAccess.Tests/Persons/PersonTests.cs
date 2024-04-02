using CarRental.DataAccess.Abstract.Circulations;
using CarRental.DataAccess.Abstract.Persons;
using CarRental.DataAccess.Repositories;
using CarRental.DataAccess.Tests.Utilities;
using CarRental.Domain.Entities.Persons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Tests.Persons
{
    [TestClass]
    public class PersonTests
    {
        private IPersonRepository _personRepository;

        public PersonTests()
        {
            _personRepository = new ApplicationRepository(ConnectionStringProvider.GetConnectionString()); ;
        }

        [DataRow("Loco quejesto", "Rodriguez", "jwnnwjncwccnk", "Qva", "573277777775")]
        [TestMethod]
        public void Can_Create_Person(string name, string lastName, string iD, string countryName, string phone)
        {
            // Arrange
            _personRepository.BeginTransaction();

            // Execute
            var personDB = _personRepository.CreateClient(name, lastName, iD);
            personDB.CountryName = countryName;
            personDB.Phone = phone;
            _personRepository.PartialCommit();
            var loadedPerson = _personRepository.GetPerson<Client>(personDB.Id);
            _personRepository.CommitTransaction();

            // Assert
            Assert.IsNotNull(loadedPerson);
            Assert.AreEqual(personDB.Name, loadedPerson.Name);
            Assert.AreEqual(personDB.LastName, loadedPerson.LastName);
            Assert.AreEqual(personDB.CI, loadedPerson.CI);
            Assert.AreEqual(personDB.CountryName, loadedPerson.CountryName);
        }

        [DataRow(1)]
        [Priority(1)]
        [TestMethod]
        public void Can_Get_Person(int id)
        {
            //Arrange
            _personRepository.BeginTransaction();

            //Execute
            var loadedPerson = _personRepository.GetPerson<Client>(id);
            _personRepository.CommitTransaction();

            //Assert
            Assert.IsNotNull(loadedPerson);
        }

        [DataRow(1, "suenha")]
        [Priority(1)]
        [TestMethod]
        public void Can_Update_Person(int id, string countryName)
        {
            // Arrange
            _personRepository.BeginTransaction();
            var loadedPerson = _personRepository.GetPerson<Client>(id);
            Assert.IsNotNull(loadedPerson);

            // Execute
            loadedPerson.CountryName = countryName;
            _personRepository.UpdatePerson(loadedPerson);
            _personRepository.PartialCommit();

            // Assert
            var modifiedPerson = _personRepository.GetPerson<Client>(id);
            _personRepository.CommitTransaction();
            Assert.AreEqual(modifiedPerson.CountryName, countryName);
        }

        [DataRow(1)]
        [Priority(30)]
        [TestMethod]
        public void Can_Delete_Person(int id)
        {
            //Arrange
            _personRepository.BeginTransaction();

            //Execute
            var loadedPerson = _personRepository.GetPerson<Client>(id);
            Assert.IsNotNull(loadedPerson);
            _personRepository.DeletePerson(loadedPerson);
            _personRepository.PartialCommit();
            loadedPerson = _personRepository.GetPerson<Client>(id);
            _personRepository.CommitTransaction();

            //Assert
            Assert.IsNull(loadedPerson);
        }
    }
}