using CarRental.DataAccess.Abstract.Common;
using CarRental.DataAccess.Abstract.Supplements;
using CarRental.DataAccess.Repositories;
using CarRental.DataAccess.Tests.Utilities;
using CarRental.Domain.Entities.Common;
using CarRental.Domain.Entities.Supplements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Tests.Supplements
{
    [TestClass]
    public class SupplementTests
    {
        private ISupplementRepository _supplementRepository;

        public SupplementTests()
        {
            _supplementRepository = new ApplicationRepository(ConnectionStringProvider.GetConnectionString());
        }

        [DataRow(1, "Que cuajoooooooooooooo")]
        [TestMethod]
        public void Can_Create_Supplement(int priceid, string description)
        {
            //Arrange
            _supplementRepository.BeginTransaction();
            Price? price = ((IPriceRepository)_supplementRepository).GetPrice(priceid);
            Assert.IsNotNull(price);

            //Execute
            _supplementRepository.CommitTransaction();
            Supplement supplement = _supplementRepository.CreateSupplement(price, description);
            _supplementRepository.PartialCommit();
            Supplement? loadedSupplement = _supplementRepository.GetSupplement(supplement.Id);

            //Assert
            Assert.IsNotNull(loadedSupplement);
            Assert.AreEqual(supplement.Price, loadedSupplement.Price);
            Assert.AreEqual(supplement.Description, loadedSupplement.Description);
        }

        [DataRow(1)]
        [TestMethod]
        public void Can_Get_Supplement(int id)
        {
            //Arrange
            _supplementRepository.BeginTransaction();

            //Execute
            var loadedSupplement = _supplementRepository.GetSupplement(id);
            _supplementRepository.CommitTransaction();

            //Assert
            Assert.IsNotNull(loadedSupplement);
        }

        //[DataRow(1,"FUCKKKKKK son las 2 AM")]
        //[TestMethod]
        //public void Can_Update_Supplement(int id, string description)
        //{
        //    //Arrange
        //    _supplementRepository.BeginTransaction();
        //    var loadedSupplement = _supplementRepository.GetSupplement(id);
        //    Assert.IsNotNull(loadedSupplement);

        //    //Execute
        //    loadedSupplement.Description = description;
        //    _supplementRepository.UpdateSupplement(loadedSupplement);

        //    //Assert
        //    var modifyedSupplement = _supplementRepository.GetSupplement(loadedSupplement.Id);
        //    _supplementRepository.CommitTransaction();
        //    Assert.AreEqual(modifyedSupplement.Description, description);

        //}

        [DataRow(1)]
        [TestMethod]
        public void Can_Delete_Supplement(int id)
        {
            //Arrange
            _supplementRepository.BeginTransaction();

            //Execute
            var loadedSupplement = _supplementRepository.GetSupplement(id);
            Assert.IsNotNull(loadedSupplement);
            _supplementRepository.DeleteSupplement(loadedSupplement);
            _supplementRepository.PartialCommit();
            loadedSupplement = _supplementRepository.GetSupplement(id);
            _supplementRepository.CommitTransaction();

            //Assert
            Assert.IsNull(loadedSupplement);

        }

    }
}
