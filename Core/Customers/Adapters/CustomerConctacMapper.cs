/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Mapper class                            *
*  Type     : CustomerContactMapper                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map customer contact.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Empiria.Trade.Core.Adapters {

  /// Methods used to map customer contact. 
  static public class CustomerConctacMapper {

    #region Public methods

    static public FixedList<CustomerContactDto> MapCustomerContacts(FixedList<CustomerContact> contacts) {
      
      var mappedList = contacts.Select((x) => MapCustomerContact(x));

      return new FixedList<CustomerContactDto>(mappedList);
    }


    static public CustomerContactDto MapCustomerContact(CustomerContact contact) {
      
      return new CustomerContactDto {
        UID = contact.UID,
        Name = contact.Name,
        Phone = contact.PhoneNumber
      };
    }

    #endregion Public methods


  } // class CustomerConctacMapper

} // namespace Empiria.Trade.Core.Adapters
