/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Test cases                              *
*  Assembly : Empiria.Trade.Test.dll                     Pattern   : Use cases tests                         *
*  Type     : CoreTests                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for core.                                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.Trade.Core.UsesCases;


namespace Empiria.Trade.Tests.Core {

  /// <summary>Test cases for core.</summary>
  public class PartyTests {

    #region Initialization

    public PartyTests() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization

    #region Facts


    [Fact]
    public void  ShouldGetPartyDtoByPartyUIDTest() {

      var usecase = PartyUseCases.UseCaseInteractor();
                
      var sut = usecase.GetParty("20120c0a-cc8a-4e12-a73a-490f418fc99f");
          
      Assert.NotNull(sut);
    }

    [Fact]
    public void ShouldGetPartyDtoByPartyIdTest() {
      var usecase = PartyUseCases.UseCaseInteractor();

      var sut = usecase.GetParty(100);

      Assert.NotNull(sut);
    }

    [Fact]
    public void ShouldGetCustomers() {
      var usecase = PartyUseCases.UseCaseInteractor();

      var sut = usecase.GetCustomers("fer  su");

      Assert.NotNull(sut);      
    }

    [Fact]
    public void ShouldGetCustomerContacts() {
      var usecase = PartyUseCases.UseCaseInteractor();

      var sut = usecase.GetCustomerContacts("rey");

      Assert.NotNull(sut);
    }

    [Fact]
    public void ShouldGetSalesAgent() {
      var usecase = PartyUseCases.UseCaseInteractor();

      var sut = usecase.GetSalesAgents();

      Assert.NotNull(sut);
    }

    [Fact]
    public void ShouldGetSuppliers() {
      var usecase = PartyUseCases.UseCaseInteractor();

      var sut = usecase.GetSuppliers("");

      Assert.NotNull(sut);
    }

    [Fact]
    public void ShouldGetInternalSuppliers() {
      var usecase = PartyUseCases.UseCaseInteractor();

      var sut = usecase.GetInternalSuppliers();

      Assert.NotNull(sut);
    }


    [Fact]
    public void ShouldGetWarehouseMan() {
      var usecase = PartyUseCases.UseCaseInteractor();

      var sut = usecase.GetWarehouseMen();

      Assert.NotNull(sut);
    }


    [Fact]
    public void ShouldGetWarehouseResponsible() {
      
      var sut = PartyUseCases.GetWarehouseResponsible();

      Assert.NotNull(sut);
    }

    #endregion Facts



  } //  public class CoreTest
} // namespace Empiria.Trade.Tests.Core
