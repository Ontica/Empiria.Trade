/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : Party                                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for parties.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Core.Data;


namespace Empiria.Trade.Core.Domain {
  
  /// <summary>Generate data for parties.  </summary>
    internal class PartyAdapter {

    #region Constructors and parsers

    protected PartyAdapter() {
    }

    #endregion Constructors and parsers


    #region Public methods


    internal static Party GetParty(string uid) {
      var party = PartyData.GetParty(uid);

      return party;
    }

    internal static Party GetParty(int partyId) {
      var party = PartyData.GetParty(partyId);

      return party;
    }


    #endregion Public methods


    #region Private methods



    #endregion Private methods

  } // class PartyAdapter

} // namespace Empiria.Trade.Core.Domain
