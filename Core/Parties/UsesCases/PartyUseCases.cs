/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Use case interactor class               *
*  Type     : CoreUseCases                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to management Parties.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Empiria.Services;
using Empiria.Trade.Core.Adapters;


namespace Empiria.Trade.Core.UsesCases {

  public class PartyUseCases : UseCase {


    #region Constructors and parsers

    protected PartyUseCases() {
      // no-op
    }

    static public PartyUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<PartyUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases

    public ShortPartyDto GetParty(string partyUID) {
      Assertion.Require(partyUID, "PartyUID");

      var party = Party.Parse(partyUID);

      return PartyMapper.MapTo(party);
    }

    public ShortPartyDto GetParty(int partyId) {
      Assertion.Require(partyId, "PartyId");

      var party = Party.Parse(partyId);

      return PartyMapper.MapTo(party);
    }

    public FixedList<NamedEntityDto> GetCustomers(string keywords) {
      return GetPartiesByRole("customer", keywords);
    }

    public FixedList<ContactDto> GetCustomerContacts(string keywords) {
      Assertion.Require(keywords, "keywords");

      var customerList = Party.GetPartiesByRole("customer", keywords);

      return PartyMapper.MapToContacs(customerList);
    }

    public FixedList<NamedEntityDto> GetSuppliers(string keywords) {
      return GetPartiesByRole("supplier", keywords);
    }  

    public FixedList<NamedEntityDto> GetSalesAgents() {
      var salesAgentsList = Party.GetSalesAgents();

      return PartyMapper.MapToMinimalPartyDto(salesAgentsList);
    }

    public FixedList<NamedEntityDto> GetInternalSuppliers() {
      var internalSuppliersList = Party.GetInternalSuppliers();

      return PartyMapper.MapToMinimalPartyDto(internalSuppliersList);
    }

    public FixedList<NamedEntityDto> GetWharehouseMan() {
      var wharehouseMan = Party.GetWharehouseMan();

      return PartyMapper.MapToMinimalPartyDto(wharehouseMan);
    }

    public FixedList<NamedEntityDto> GetAccountHolders(string keywords) {
      return GetPartiesByRole(string.Empty, keywords);
    }

    #endregion Use cases

    #region Private methods

    private FixedList<NamedEntityDto> GetPartiesByRole(string role, string keywords) {
      Assertion.Require(keywords, "keywords");

      var partyList = Party.GetPartiesByRole(role, keywords);

      return PartyMapper.MapToMinimalPartyDto(partyList);
    }

  
    #endregion Private methods

  }
}
