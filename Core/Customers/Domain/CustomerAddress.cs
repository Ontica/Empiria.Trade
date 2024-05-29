/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : PartyContact                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a addresses for customers.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Data;

namespace Empiria.Trade.Core {

  ///  Represents a addresses for customers.
  public class CustomerAddress : BaseObject {

    #region Constructors and parsers

    public CustomerAddress() {
      //no-op
    }

    public static CustomerAddress Parse(int id) => BaseObject.ParseId<CustomerAddress>(id);

    public static CustomerAddress Parse(string uid) => BaseObject.ParseKey<CustomerAddress>(uid);

    public static CustomerAddress Empty => BaseObject.ParseEmpty<CustomerAddress>();


    #endregion Constructors and parsers

    #region Public properties

    [DataField("PartyId")]
    public int CustomerId {
      get; set;
    }

    [DataField("PartyAddressIndex")]
    public int Index {
      get; set;
    }

    [DataField("PartyAddessDescription")]
    public string Description {
      get; set;
    }

    [DataField("Address1")]
    public string Address1 {
      get; set;
    }

    [DataField("Address2")]
    public string Address2 {
      get; set;
    }

    [DataField("City")]
    public string City {
      get; set;
    }

    [DataField("State")]
    public string State {
      get; set;
    }

    [DataField("ZipCode")]
    public string ZipCode {
      get; set;
    }

    [DataField("PartyAddessExtData")]
    public string ExtData {
      get; set;
    } = String.Empty;

    [DataField("PartyAddessStatus", Default = 'A')]
    public char Status {
      get; set;
    } = 'A';

    #endregion Public properties

    #region Public methods


    protected override void OnSave() {
      CustomerAddressData.Write(this);
    }


    public FixedList<CustomerAddress> GetAddresses(int customerId) {
      return CustomerAddressData.GetCustomerAddresses(customerId);
    }


    #endregion Public methods

  } // class CustomerAddress

} // namespace Empiria.Trade.Core