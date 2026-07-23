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

    [DataField("Party_Id")]
    public int CustomerId {
      get; private set;
    }

    [DataField("Party_Contact_Index")]
    public int Index {
      get; private set;
    }

    [DataField("Party_Contact_Name")]
    public string Name {
      get; private set;
    }

    [DataField("Party_Contact_Email")]
    public string Email {
      get; private set;
    } = String.Empty;

    [DataField("Party_Contact_Phone_Number")]
    public string PhoneNumber {
      get; private set;
    } = String.Empty;

    [DataField("Party_Contact_Ext_Data")]
    public string ExtData {
      get; private set;
    } = String.Empty;

    //TODO CAMBIAR TIPO A UN ENUM DE EMPIRIA
    [DataField("Party_Contact_Status", Default = 'A')]
    public char Status {
      get; private set;
    } = 'A';


    #endregion Public properties

    #region Public methods

    protected override void OnSave() {
      CustomerContactData.Write(this);
    }

    static public FixedList<CustomerContact> GetContacts(int customerId) {
      return CustomerContactData.GetCustomerContacts(customerId);     
    }

    #endregion


  }  //class CustomerContact

} // namespace Empiria.Trade.Core
