/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : CustomerContactData                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for customer contacts.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.Trade.Core.Data {

  /// Provides data read methods for customer contacts.
  static internal class CustomerContactData {

    #region Internal methods 

    static internal FixedList<CustomerContact> GetCustomerContacts(int customerId) {

      var sql = "SELECT * " +
                "FROM TRDPartyContacts " +
               $"WHERE PartyId = {customerId} AND PartyContactStatus = 'A' ";

      var dataOperation = DataOperation.Parse(sql);


      return DataReader.GetFixedList<CustomerContact>(dataOperation);
    }

    static internal void Write(CustomerContact o) {
      var op = DataOperation.Parse("writePartyContacts", o.Id, o.UID, o.CustomerId, o.Index, o.Name,
                                   o.Email, o.PhoneNumber, o.ExtData, o.Status);
      DataWriter.Execute(op);
    }

    #endregion Internal methods


  } //class CustomerContactData


} // namespace Empiria.Trade.Core.Data
