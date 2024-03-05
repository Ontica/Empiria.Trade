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
      var dto = new CustomerAddressDto {
        UID = address.UID,
        Address = address.Address1,
        Neighborhood = address.Address2,
        City = address.City,
        State = address.State,
        ZipCode = address.ZipCode
      };


      return dto;
    }

    static public FixedList<CustomerShortAddressDto> MapToShortAddresses(FixedList<CustomerAddress> addresses) {
      List<CustomerShortAddressDto> CustomerAddressDtoList = new List<CustomerShortAddressDto>();

      int count = 0;

      foreach (var address in addresses) {
        if (count == 0) {
          CustomerAddressDtoList.Add(MapShortAddress(address));
        } else {
          var dto = new CustomerShortAddressDto {
            UID = address.UID,
            Name = "Sucursal " +  address.Description,
            Description = address.Address1 + ", " + address.Address2 + " " + address.City + ", " + address.State, 
          };

          CustomerAddressDtoList.Add(dto);
        }
        
        count++;
      }      

      return CustomerAddressDtoList.ToFixedList();
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
      List<CustomerAddressDto> CustomerAddressDtoList = new List<CustomerAddressDto>();

      foreach (var address in addresses) {
        CustomerAddressDtoList.Add(MapTo(address));
      }

      return CustomerAddressDtoList.ToFixedList();
    }


    #endregion Public methods

  } // class CustomerAddressMapper


} // namespace Empiria.Trade.Core.Adapters
