using CarRental.DataAccess.Abstract.Circulations;
using CarRental.DataAccess.Abstract.Common;
using CarRental.DataAccess.Abstract.Persons;
using CarRental.DataAccess.Abstract.Reservations;
using CarRental.DataAccess.Abstract.Supplements;
using CarRental.DataAccess.Abstract.Vehicles;
using CarRental.DataAccess.Repositories;
using CarRental.DataAccess.Tests.Utilities;
using CarRental.Domain.Entities.Circulations;
using CarRental.Domain.Entities.Common;
using CarRental.Domain.Entities.Persons;
using CarRental.Domain.Entities.Reservations;
using CarRental.Domain.Entities.Supplements;
using CarRental.Domain.Entities.Types;
using CarRental.Domain.Entities.Vehicles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Tests.Reservations
{
    [TestClass]
    public class ReservationTests
    {
        private IReservationRepository _reservationRepository;

        public ReservationTests()
        {
            _reservationRepository = new ApplicationRepository(ConnectionStringProvider.GetConnectionString());
        }

        [DataRow(1, 1, "2024-03-16T12:00:00", 1,Status.Cancelled, 1)]
        [TestMethod]
        public void Can_Create_Reservation(int clientId, int vehicleId, string startDateString, int priceId, Status status, int supplementId)
        {
            DateTime startDate = DateTime.Parse(startDateString);
            //Arrange
            _reservationRepository.BeginTransaction();
            Client? client = ((IPersonRepository)_reservationRepository).GetPerson<Client>(clientId);
            Assert.IsNotNull(client);
            Car ? vehicle = ((IVehicleRepository)_reservationRepository).GetVehicle<Car>(vehicleId);
            Assert.IsNotNull(vehicle);
            Price ? totalPrice = ((IPriceRepository)_reservationRepository).GetPrice(priceId);
            Assert.IsNotNull(totalPrice);
            Supplement? reservationSupplement = ((ISupplementRepository)_reservationRepository).GetSupplement(supplementId);
            Assert.IsNotNull(reservationSupplement);

            //Execute
            var reservationDB = _reservationRepository.CreateReservation(client, vehicle, startDate, totalPrice, status, reservationSupplement);
            _reservationRepository.PartialCommit();
            Reservation? loadedReservation = _reservationRepository.GetReservation(reservationDB.Id);
            _reservationRepository.CommitTransaction();

            //Assert
            Assert.IsNotNull(reservationDB);
            Assert.AreEqual(reservationDB.Client , loadedReservation.Client);
            Assert.AreEqual(reservationDB.Vehicle, loadedReservation.Vehicle);
            Assert.AreEqual(reservationDB.TotalPrice, loadedReservation.TotalPrice);
            Assert.AreEqual(reservationDB.ReservationSupplement, loadedReservation.ReservationSupplement);
            Assert.AreEqual(reservationDB.Status, loadedReservation.Status);
        }
        
        
        [DataRow(1)]
        [TestMethod]
        public void Can_Get_Reservation(int id)
        {
            //Arrange
            _reservationRepository.BeginTransaction();

            //Execute
            var loadedReservation = _reservationRepository.GetReservation(id);
            _reservationRepository.CommitTransaction();

            //Assert
            Assert.IsNotNull(loadedReservation);

        }

        [DataRow(1, Status.Approved)]
        [TestMethod]
        public void Can_Update_Reservation(int id, Status status, string startDateString)
        {
            DateTime startDate = DateTime.Parse(startDateString);

            //Arrange
            _reservationRepository.BeginTransaction();
            var loadedReservation = _reservationRepository.GetReservation(id);
            Assert.IsNotNull(loadedReservation);

            //Execute
            loadedReservation.Status = status;
            _reservationRepository.UpdateReservation(loadedReservation);

            //Assert
            Reservation? modifyedReservation = _reservationRepository.GetReservation(id);
            _reservationRepository.CommitTransaction();
            Assert.AreEqual(modifyedReservation.Status, status);
            Assert.AreEqual(modifyedReservation.StartDate, startDate);

        }

        [DataRow(1)]
        [TestMethod]
        public void Can_Delete_Reservation(int id)
        {
            //Arrange
            _reservationRepository.BeginTransaction();

            //Execute
            var loadedReservation = _reservationRepository.GetReservation(id);
            Assert.IsNotNull(loadedReservation);
            _reservationRepository.DeleteReservation(loadedReservation);
            _reservationRepository.PartialCommit();
            loadedReservation = _reservationRepository.GetReservation(id);
            _reservationRepository.CommitTransaction();

            //Assert
            Assert.IsNull(loadedReservation);

        }
    }

}
