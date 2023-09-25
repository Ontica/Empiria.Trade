﻿/* Empiria Trade *********************************************************************************************
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

using Empiria.Tests;

using Empiria.Trade.Core.UsesCases;


namespace Empiria.Trade.Tests.Core {

  /// <summary>Test cases for core.</summary>
  public class CoreTests {

    #region Initialization

    public CoreTests() {
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

      var sut = usecase.GetParty(1);

      Assert.NotNull(sut);
    }

    
    #endregion Facts



  } //  public class CoreTest
} // namespace Empiria.Trade.Tests.Core