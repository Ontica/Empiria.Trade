using System;
using System.Collections.Generic;
using ConnectionsToFirebirdSujetsa.Adapters;
using ConnectionsToFirebirdSujetsa.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SujetsaOldDataSchemaManagerTest.ConnectionTests {

  [TestClass]
  class DataConectionsTest {

    [TestMethod]
    public void GetDataCountFromDbTest() {

      var service = new Services();
      var dt = service.GetDataCountFromDb();

      Assert.IsTrue(true);

    }

    [TestMethod]
    public void InsertTest() {

      var service = new Services();

      List<ProductosAdapter> productsToUpdate = service.GetDataFromDb();

      string message = service.InsertProductToSql(productsToUpdate);

      Assert.IsTrue(true);

    }

  }
}
