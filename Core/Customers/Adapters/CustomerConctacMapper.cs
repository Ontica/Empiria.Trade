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

namespace Empiria.Trade.Core.Adapters {

  /// Methods used to map customer contact. 
  static public class CustomerConctacMapper {

    #region Public methods

    static public FixedList<CustomerContactDto> MapCustomerContacts(FixedList<CustomerContact> contacts) {
      List<CustomerContactDto> contactsList = new List<CustomerContactDto>();

      foreach (var contact in contacts) {
        contactsList.Add(MapCustomerContact(contact));
      }

      return contactsList.ToFixedList();
    }

   static public CustomerContactDto MapCustomerContact(CustomerContact contact) {
      var dto = new CustomerContactDto {
        UID = contact.UID,
        Name = contact.Name,
        Phone = contact.PhoneNumber
      };


      return dto;
    }

    #endregion Public methods


  } // class CustomerConctacMapper

} // namespace Empiria.Trade.Core.Adapters
