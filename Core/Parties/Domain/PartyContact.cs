/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : PartyContact                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a PartyContact.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Core.Data;
using Empiria.Json;


namespace Empiria.Trade.Core.Domain {

  /// <summary>Represents a PartyContact.  </summary>
  internal class PartyContact {

    #region Constructors and parsers

    private PartyContact() {
      //no-op
    }

    static public PartyContact Parse(JsonObject json) {
      Assertion.Require(json, nameof(json));

      return new PartyContact {
        Index = json.Get<int>("Index"),
        Name = json.Get<string>("Name"),
        Email = json.Get<string>("Email"),
        PhoneNumber = json.Get<string>("PhoneNumber")
      };
    }

    #endregion Constructors and parsers

    #region Public properties
    public int Index {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Email {
      get; internal set;
    }

    public string PhoneNumber {
      get; internal set;
    }

    #endregion Public properties


  } // internal class PartyContact 
} // namespace Empiria.Trade.Core.Domain
