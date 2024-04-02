﻿using CarRental.DataAccess.Abstract.Common;
using CarRental.DataAccess.Repositories;
using CarRental.DataAccess.Tests.Utilities;
using CarRental.Domain.Entities.Common;
using CarRental.Domain.Entities.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Tests.Common
{
    [TestClass]
    public class PriceTests
    {
        private IPriceRepository _priceRepository;

        public PriceTests()
        {
            _priceRepository = new ApplicationRepository(ConnectionStringProvider.GetConnectionString());
        }

        [DataRow(MoneyType.MN, 50000)]
        [DataRow(MoneyType.Euro, 6000)]
        [TestMethod]
        public void Can_Create_Price(MoneyType moneyType, double value)
        {
            //Arrange
            _priceRepository.BeginTransaction();

            //Execute
            Price newPrice = _priceRepository.CreatePrice(moneyType, value);
            _priceRepository.PartialCommit();
            Price? loadedPrice = _priceRepository.GetPrice(newPrice.Id);
            _priceRepository.CommitTransaction();

            //Assert
            Assert.IsNotNull(loadedPrice);
            Assert.AreEqual(loadedPrice.Currency, newPrice.Currency);
            Assert.AreEqual(loadedPrice.Value, newPrice.Value);
        }

        [DataRow(1)]
        [Priority(1)]
        [TestMethod]
        public void Can_Get_Price(int id)
        {
            //Arrange
            _priceRepository.BeginTransaction();

            //Execute
            var loadedPrice = _priceRepository.GetPrice(id);
            _priceRepository.CommitTransaction();

            //Assert
            Assert.IsNotNull(loadedPrice);
        }

        [DataRow(1, MoneyType.USD, 6200)]
        [Priority(1)]
        [TestMethod]
        public void Can_Update_Price(int id, double value, MoneyType moneyType)
        {
            //Arrange
            _priceRepository.BeginTransaction();
            var loadedPrice = _priceRepository.GetPrice(id);
            Assert.IsNotNull(loadedPrice);

            //Execute
            loadedPrice.Value = value;
            loadedPrice.Currency = moneyType;
            _priceRepository.UpdatePrice(loadedPrice);

            //Assert
            var modifyedPrice = _priceRepository.GetPrice(id);
            _priceRepository.CommitTransaction();
            Assert.AreEqual(modifyedPrice.Value, value);
            Assert.AreEqual(modifyedPrice.Currency, moneyType);
        }

        [DataRow(1)]
        [Priority(30)]
        [TestMethod]
        public void Can_Delete_Price(int id)
        {
            //Arrange
            _priceRepository.BeginTransaction();

            //Execute
            var loadedPrice = _priceRepository.GetPrice(id);
            Assert.IsNotNull(loadedPrice);
            _priceRepository.DeletePrice(loadedPrice);
            _priceRepository.PartialCommit();
            loadedPrice = _priceRepository.GetPrice(id);
            _priceRepository.CommitTransaction();

            //Assert
            Assert.IsNull(loadedPrice);
        }
    }
}