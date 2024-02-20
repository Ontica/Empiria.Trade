/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : CustomerAddressData                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for customer address.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.Trade.Core.Data {


  /// Provides data read methods for customer address.
  internal static  class CustomerAddressData {

    #region Internal methods 


    internal static FixedList<CustomerAddress> GetCustomerAddresses(int customerId) {
           
      var sql = "SELECT * " +
                "FROM TRDPartyAddresses " +
               $"WHERE PartyId = {customerId} AND PartyAddessStatus = 'A' ";

      var dataOperation = DataOperation.Parse(sql);


      return DataReader.GetFixedList<CustomerAddress>(dataOperation);
    }

    static internal void Write(CustomerAddress o) {
      var op = DataOperation.Parse("writePartyAddress", o.Id, o.UID, o.CustomerId, o.Index, o.Description,
                                   o.Address1, o.Address2, o.City, o.State, o.ZipCode, o.ExtData, o.Status);
      DataWriter.Execute(op);
    }

    #endregion Internal methods

  } // class CustomerAddressData

} // namespace Empiria.Trade.Core.Data
