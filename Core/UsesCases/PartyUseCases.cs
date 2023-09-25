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

    #endregion Use cases


  }
}
