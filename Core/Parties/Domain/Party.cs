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

using Empiria.Json;
using Empiria.Trade.Core.Data;

namespace Empiria.Trade.Core {

  /// <summary>Represent Party</summary>
  public class Party : BaseObject, INamedEntity {

    #region Constructors and parsers

    public Party() {
      //no-op
    }

    public static Party Parse(int id) => BaseObject.ParseId<Party>(id);

    public static Party Parse(string uid) => BaseObject.ParseKey<Party>(uid);

    public static Party Empty => BaseObject.ParseEmpty<Party>();


    #endregion Constructors and parsers

    #region Public properties

   
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

    [DataField("PartyContacts", IsOptional = true)]
    private JsonObject ContactsJson {
      get;
      set;
    } = new JsonObject();

    internal FixedList<PartyContact> Contacts {
      get {
        return this.ContactsJson.GetFixedList<PartyContact>("contacts", false);
      }
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

    internal static FixedList<Party> GetPartiesByRole(string role, string keywords) {
      return PartyData.GetPartyListByRole(role, keywords);
    }

    internal static FixedList<Party> GetInternalSuppliers() {
      return PartyData.GetPartyListByRole("internalSupplier");
    }

    internal static FixedList<Party> GetSalesAgents() {
      return PartyData.GetPartyListByRole("salesAgent");
    }

    internal static FixedList<Party> GetWharehouseMan() {
      return PartyData.GetPartyListByRole("whareHouseMan");
    }

    #endregion Public methos

    #region Private methods


    #endregion Private methods

  } // internal class Party

 

} // namespace Empiria.Trade.Core.Domain
