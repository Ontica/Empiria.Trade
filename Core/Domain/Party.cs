/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : Party                                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Party.                                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Core.Data;

namespace Empiria.Trade.Core.Domain {

  /// <summary>Represent Party</summary>
  internal class Party {

    #region Constructors and parsers

    public Party() {
      //no-op
    }

    internal static Party Parse(int id) => PartyData.GetParty(id);

    internal static Party Parse(string uid) => PartyData.GetParty(uid);


    #endregion Constructors and parsers

    #region Public properties

    [DataField("PartyId")]
    public int PartyId {
      get;
      private set;
    }

    [DataField("PartyUID")]
    public string PartyUID {
      get;
      private set;
    }

    [DataField("PartyName")]
    public string PartyName {
      get;
      private set;
    }

    [DataField("PartyShortName")]
    public string PartyShortName {
      get;
      private set;
    }
    
    [DataField("PartyAddressLine1")]
    public string PartyAddressLine1 {
      get;
      private set;
    }

    [DataField("PartyAddressLine2")]
    public string PartyAddressLine2 {
      get;
      private set;
    }

    [DataField("PartyLocationId")]
    public int PartyLocationId {
      get;
      private set;
    }

    [DataField("PartyZipCode")]
    public string PartyZipCode {
      get;
      private set;
    }

    [DataField("PartyEMail")]
    public string PartyEMail {
      get;
      private set;
    }

    [DataField("PartyPhoneNumbers")]
    public string PartyPhoneNumbers {
      get;
      private set;
    }

    [DataField("PartyContacts")]
    public string PartyContacts {
      get;
      private set;
    }  
        
    [DataField("PartyTaxationID")]
    public string PartyTaxationID {
      get;
      private set;
    }

    [DataField("PartyKeywords")]
    public string PartyKeywords {
      get;
      private set;
    }

    [DataField("PartyExtData")]
    public string PartyExtData {
      get;
      private set;
    }

    [DataField("PartyStatus")]
    public char PartyStatus {
      get;
      private set;
    }


    #endregion Public properties

    #region Public methods


    #endregion Public methos

    #region Private methods
      

    #endregion Private methods

  } // internal class Party

} // namespace Empiria.Trade.Core.Domain
