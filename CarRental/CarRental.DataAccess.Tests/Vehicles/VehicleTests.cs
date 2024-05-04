using CarRental.DataAccess.Abstract.Common;
using CarRental.DataAccess.Abstract.Insurances;
using CarRental.DataAccess.Abstract.Somatons;
using CarRental.DataAccess.Abstract.Vehicles;
using CarRental.DataAccess.Repositories;
using CarRental.DataAccess.Tests.Utilities;
using CarRental.Domain.Entities.Common;
using CarRental.Domain.Entities.Insurances;
using CarRental.Domain.Entities.Somatons;
using CarRental.Domain.Entities.Vehicles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Tests.Vehicles
{
    [TestClass]
    public class VehicleTests
    {
        private IVehicleRepository _vehicleRepository;

        public VehicleTests()
        {
            _vehicleRepository = new ApplicationRepository(ConnectionStringProvider.GetConnectionString());
        }

        [DataRow("Lada", "Jan 1, 2009", 1, 1, 1, 1)]
        [Priority(2)]
        [TestMethod]
        public void Can_Create_Vehicle(string brandName, string fabricationDateString, int insuranceId, int somatonId, int circulationId, int priceId)
        {
            DateTime fabricationDate = DateTime.Parse(fabricationDateString);
            // Arrange
            _vehicleRepository.BeginTransaction();
            Insurance? insurance = ((IInsuranceRepository)_vehicleRepository).GetInsurance(insuranceId);
            Assert.IsNotNull(insurance);
            Somaton? somaton = ((ISomatonRepository)_vehicleRepository).GetSomaton(somatonId);
            Assert.IsNotNull(somaton);
            Price? price = ((IPriceRepository)_vehicleRepository).GetPrice(priceId);
            Assert.IsNotNull(price);

            // Execute
            var vehicleDB = _vehicleRepository.CreateCar(brandName, fabricationDate, insurance, somaton);
            vehicleDB.BrandName = brandName;
            vehicleDB.SomatonId = somatonId;
            vehicleDB.InsuranceId = insuranceId;
            vehicleDB.PriceId = priceId;
            vehicleDB.CirculationId = circulationId;
            _vehicleRepository.PartialCommit();

            var loadedVehicle = _vehicleRepository.GetVehicle<Car>(vehicleDB.Id);
            _vehicleRepository.CommitTransaction();

            // Assert
            Assert.IsNotNull(loadedVehicle);
            Assert.AreEqual(vehicleDB.BrandName, loadedVehicle.BrandName);
            Assert.AreEqual(vehicleDB.FabricationDate, loadedVehicle.FabricationDate);
        }

        [DataRow(1)]
        [Priority(3)]
        [TestMethod]
        public void Can_Get_Vehicle(int id)
        {
            //Arrange
            _vehicleRepository.BeginTransaction();

            //Execute
            var loadedVehicle = _vehicleRepository.GetVehicle<Car>(id);
            _vehicleRepository.CommitTransaction();

            //Assert
            Assert.IsNotNull(loadedVehicle);
        }

        [DataRow(1, false)]
        [Priority(3)]
        [TestMethod]
        public void Can_Update_Vehicle(int id, bool hasAirConditioning)
        {
            // Arrange
            _vehicleRepository.BeginTransaction();
            var loadedVehicle = _vehicleRepository.GetVehicle<Car>(id);
            Assert.IsNotNull(loadedVehicle);

            // Execute
            loadedVehicle.HasAirConditioning = hasAirConditioning;
            _vehicleRepository.UpdateVehicle(loadedVehicle);
            _vehicleRepository.PartialCommit();

            // Assert
            var modifiedVehicle = _vehicleRepository.GetVehicle<Car>(id);
            _vehicleRepository.CommitTransaction();
            Assert.AreEqual(modifiedVehicle.HasAirConditioning, hasAirConditioning);
        }

        [DataRow(1)]
        [Priority(28)]
        [TestMethod]
        public void Can_Delete_Vehicle(int id)
        {
            //Arrange
            _vehicleRepository.BeginTransaction();

            //Execute
            var loadedVehicle = _vehicleRepository.GetVehicle<Car>(id);
            Assert.IsNotNull(loadedVehicle);
            _vehicleRepository.DeleteVehicle(loadedVehicle);
            _vehicleRepository.PartialCommit();
            loadedVehicle = _vehicleRepository.GetVehicle<Car>(id);
            _vehicleRepository.CommitTransaction();

            //Assert
            Assert.IsNull(loadedVehicle);
        }
    }
}