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
using Empiria.Trade.Core.Domain;

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

      var party = PartyAdapter.GetParty(partyUID);

      return PartyMapper.MapTo(party);
    }

    public ShortPartyDto GetParty(int partyId) {
      Assertion.Require(partyId, "PartyId");

      var party = PartyAdapter.GetParty(partyId);

      return PartyMapper.MapTo(party);
    }

    public FixedList<NamedEntityDto> GetCustomers(string keywords) {
      return GetPartiesByRole("customer", keywords);
    }

    public FixedList<NamedEntityDto> GetCustomerContacts(string customerUID) {
      return GetPartiesByRole("customerContact", customerUID);
    }

    public FixedList<NamedEntityDto> GetSuppliers(string keywords) {
      return GetPartiesByRole("supplier", keywords);
    }

    public FixedList<NamedEntityDto> GetSalesAgents(string keywords) {
      return GetPartiesByRole("salesAgent", keywords);
    }

    public FixedList<NamedEntityDto> GetInternalSuppliers() {
      var internalSuppliersList = PartyAdapter.GetInternalSuppliers();

      return PartyMapper.MapToMinimalPartyDto(internalSuppliersList);
    }

    #endregion Use cases

    #region Private methods

    private FixedList<NamedEntityDto> GetPartiesByRole(string role, string keywords) {
      Assertion.Require(keywords, "keywords");

      var partyList = PartyAdapter.GetPartiesByRole(role, keywords);

      return PartyMapper.MapToMinimalPartyDto(partyList);
    }

  
    #endregion Private methods

  }
}
