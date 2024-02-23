/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : PartyContact                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a contact for customers.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Core.Data;
using Empiria.Trade.Core.Domain;


namespace Empiria.Trade.Core {

  /// Represents a contact for customers.
  public class CustomerContact : BaseObject {

    #region Constructors and parsers

    public CustomerContact() {
      //no-op
    }

    public static CustomerContact Parse(int id) => BaseObject.ParseId<CustomerContact>(id);

    public static CustomerContact Parse(string uid) => BaseObject.ParseKey<CustomerContact>(uid);

    

    public static CustomerContact Empty => BaseObject.ParseEmpty<CustomerContact>();


    #endregion Constructors and parsers

    #region Public properties

    [DataField("PartyId")]
    public int CustomerId {
      get; private set;
    }

    [DataField("PartyContactIndex")]
    public int Index {
      get; private set;
    }

    [DataField("PartyContactName")]
    public string Name {
      get; private set;
    }

    [DataField("PartyContactEmail")]
    public string Email {
      get; private set;
    } = String.Empty;

    [DataField("PartyContactPhoneNumber")]
    public string PhoneNumber {
      get; private set;
    } = String.Empty;

    [DataField("PartyContactExtData")]
    public string ExtData {
      get; private set;
    } = String.Empty;

    [DataField("PartyContactStatus", Default = 'A')]
    public char Status {
      get; private set;
    } = 'A';


    #endregion Public properties

    #region Public methods

    protected override void OnSave() {
      CustomerContactData.Write(this);
    }

    public FixedList<CustomerContact> GetContacts(int customerId) {
      return CustomerContactData.GetCustomerContacts(customerId);     
    }

    #endregion


  }  //class CustomerContact

} // namespace Empiria.Trade.Core
