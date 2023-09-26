﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Test cases                              *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Use cases tests                         *
*  Type     : ProductTests                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for products.                                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.Tests;
using Empiria.Trade.Inventory.Products.UseCases;
using Empiria.Trade.Inventory.Products.Adapters;
using System.Threading.Tasks;
using Empiria.DataTypes;

namespace Empiria.Trade.Tests {

  /// <summary>Test cases for products.</summary>
  public class TRDProductTests {

    #region Initialization

    public TRDProductTests() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts


    [Fact]
    public void ShouldGetProductTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      string uid = "UID-AOME1-31161500";

      TRDProductsEntryDto sut = usecase.GetTRDProduct(uid);
        
      Assert.NotNull(sut);
      
    }


    #endregion Facts


  } // class ProductTests

} // namespace Empiria.Trade.Tests.Products
