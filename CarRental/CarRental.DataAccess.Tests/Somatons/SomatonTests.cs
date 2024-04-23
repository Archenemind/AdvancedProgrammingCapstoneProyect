using CarRental.DataAccess.Abstract.Somatons;
using CarRental.DataAccess.Repositories;
using CarRental.DataAccess.Tests.Utilities;
using CarRental.Domain.Entities.Somatons;
using CarRental.Domain.Entities.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Tests.Somatons
{
    [TestClass]
    public class SomatonTests
    {
        private ISomatonRepository _somatonRepository;

        public SomatonTests()
        {
            _somatonRepository = new ApplicationRepository(ConnectionStringProvider.GetConnectionString());
        }

        [DataRow("2024-03-16T12:00:00", "2020-03-16T12:00:00", Status.Cancelled, "bfuebfebfqfjq")]
        [Priority(1)]
        [TestMethod]
        public void Can_Create_Somaton(string expirationDateString, string expeditionDateString, Status status, string number)
        {
            DateTime expirationDate = DateTime.Parse(expirationDateString);
            DateTime expeditionDate = DateTime.Parse(expeditionDateString);

            //Arrange
            _somatonRepository.BeginTransaction();

            //Execute
            var somatonDB = _somatonRepository.CreateSomaton(expirationDate, expeditionDate, status, number);
            _somatonRepository.PartialCommit();
            var loadedSomaton = _somatonRepository.GetSomaton(somatonDB.Id);
            _somatonRepository.CommitTransaction();

            //Assert
            Assert.IsNotNull(loadedSomaton);
            Assert.AreEqual(loadedSomaton.ExpirationDate, expirationDate);
            Assert.AreEqual(loadedSomaton.ExpeditionDate, expeditionDate);
            Assert.AreEqual(loadedSomaton.Status, status);
            Assert.AreEqual(loadedSomaton.Number, number);
        }

        [DataRow(1)]
        [Priority(2)]
        [TestMethod]
        public void Can_Get_Somaton(int id)
        {
            //Arrange
            _somatonRepository.BeginTransaction();

            //Execute
            var loadedSomaton = _somatonRepository.GetSomaton(id);
            _somatonRepository.CommitTransaction();

            //Assert
            Assert.IsNotNull(loadedSomaton);
        }

        [DataRow(1, Status.Consumed)]
        [Priority(2)]
        [TestMethod]
        public void Can_Update_Somaton(int id, Status status)
        {
            //Arrange
            _somatonRepository.BeginTransaction();
            Somaton loadedSomaton = _somatonRepository.GetSomaton(id);
            Assert.IsNotNull(loadedSomaton);

            //Execute
            loadedSomaton.Status = status;
            _somatonRepository.UpdateSomaton(loadedSomaton);

            //Assert
            Somaton modifyedSomaton = _somatonRepository.GetSomaton(id);
            _somatonRepository.CommitTransaction();
            Assert.AreEqual(modifyedSomaton.Status, status);
        }

        [DataRow(1)]
        [Priority(30)]
        [TestMethod]
        public void Can_Delete_Somaton(int id)
        {
            //Arrange
            _somatonRepository.BeginTransaction();

            //Execute
            var loadedSomaton = _somatonRepository.GetSomaton(id);
            Assert.IsNotNull(loadedSomaton);
            _somatonRepository.DeleteSomaton(loadedSomaton);
            _somatonRepository.PartialCommit();
            loadedSomaton = _somatonRepository.GetSomaton(id);
            _somatonRepository.CommitTransaction();

            //Assert
            Assert.IsNull(loadedSomaton);
        }
    }
}