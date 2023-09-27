﻿/* Empiria Trade *********************************************************************************************
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
  internal class Party: INamedEntity {

    #region Constructors and parsers

    public Party() {
      //no-op
    }

    internal static Party Parse(int id) => PartyData.GetParty(id);

    internal static Party Parse(string uid) => PartyData.GetParty(uid);


    #endregion Constructors and parsers

    #region Public properties

    [DataField("PartyId")]
    public int Id {
      get;
      private set;
    }

    [DataField("PartyUID")]
    public string UID {
      get;
      private set;
    }

    [DataField("PartyName")]
    public string Name {
      get;
      private set;
    }

    [DataField("PartyShortName")]
    public string ShortName {
      get;
      private set;
    }
    
    [DataField("PartyAddressLine1")]
    public string AddressLine1 {
      get;
      private set;
    }

    [DataField("PartyAddressLine2")]
    public string AddressLine2 {
      get;
      private set;
    }

    [DataField("PartyLocationId")]
    public int LocationId {
      get;
      private set;
    }

    [DataField("PartyZipCode")]
    public string ZipCode {
      get;
      private set;
    }

    [DataField("PartyEMail")]
    public string EMail {
      get;
      private set;
    }

    [DataField("PartyPhoneNumbers")]
    public string PhoneNumbers {
      get;
      private set;
    }

    [DataField("PartyContacts")]
    public string Contacts {
      get;
      private set;
    }  
        
    [DataField("PartyTaxationID")]
    public string TaxationID {
      get;
      private set;
    }

    [DataField("PartyKeywords")]
    public string Keywords {
      get;
      private set;
    }

    [DataField("PartyExtData")]
    public string ExtData {
      get;
      private set;
    }

    [DataField("PartyStatus")]
    public string Status {
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
