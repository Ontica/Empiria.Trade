﻿
/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Mapper class                            *
*  Type     : PartyMapper                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Parties.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Core.Domain;


namespace Empiria.Trade.Core.Adapters {

  /// <summary>Methods used to map Parties.   </summary>
  static internal class PartyMapper {


    #region Public methods

    static internal ShortPartyDto MapTo(Party party) {
      var dto = new ShortPartyDto  {
        id = party.Id,
        UID = party.UID,
        Name = party.Name,
        Phone = party.PhoneNumbers
      };


      return dto;
    }

    #endregion Public methods


    #region Private methods

    #endregion Private methods

  } //static internal class PartyMapper

} // namespace Empiria.Trade.Core.Adapters