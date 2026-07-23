/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Mapper class                            *
*  Type     : CustomerAddressMapper                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map customer address.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;


namespace Empiria.Trade.Core.Adapters {

  /// Methods used to map customer address.
  static public class CustomerAddressMapper {


    #region Public methods

    static public CustomerAddressDto MapTo(CustomerAddress address) {
      
      return new CustomerAddressDto {
        UID = address.UID,
        Address = address.Address1,
        Neighborhood = address.Address2,
        City = address.City,
        State = address.State,
        ZipCode = address.ZipCode
      };
    }


    static public FixedList<CustomerShortAddressDto> MapCustomerShortAddresses(
                                                      FixedList<CustomerAddress> addresses) {

      var mappedList = addresses.Select((x) => MapShortAddress(x));

      return new FixedList<CustomerShortAddressDto>(mappedList);
    }


    static public CustomerShortAddressDto MapShortAddress(CustomerAddress address) {
      var dto = new CustomerShortAddressDto {
        UID = address.UID,
        Name = address.Description,
        Description = address.Address1 + ", " + address.Address2 + " " + address.City + ", " + address.State
      };

      return dto;
    }


    static public FixedList<CustomerAddressDto> MapCustomerAddresses(FixedList<CustomerAddress> addresses) {
      
      var mappedList = addresses.Select((x) => MapTo(x));

      return new FixedList<CustomerAddressDto>(mappedList);
    }


    #endregion Public methods

  } // class CustomerAddressMapper


} // namespace Empiria.Trade.Core.Adapters
