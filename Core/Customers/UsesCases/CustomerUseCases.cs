/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Orders.dll                   Pattern   : Use case interactor class               *
*  Type     : CustomerUseCases                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to management Customers.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;
using Empiria.Trade.Core.Adapters;

namespace Empiria.Trade.Core.UsesCases {

  /// Use cases used to management Customers.
  public class CustomerUseCases : UseCase {

    #region Constructors and parsers

    protected CustomerUseCases() {
      // no-op
    }

    static public CustomerUseCases UseCaseInteractor() {
      return CreateInstance<CustomerUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases

    public FixedList<CustomerShortAddressDto> GetCustomerAddress(string customerUID) {
      var customer = new CustomerAddress();

      Party customerInfo= Party.Parse(customerUID);

      var addresses = customer.GetAddresses(customerInfo.Id);

      var addressesDto = CustomerAddressMapper.MapToShortAddresses(addresses);

      return addressesDto;
    }

    public  FixedList<CustomerContactDto> GetCustomerContacts(int customerId) {
      var contact = new CustomerContact();
     
      var contacts = contact.GetContacts(customerId);

      return CustomerConctacMapper.MapCustomerContacts(contacts);
    }

    #endregion Use cases

  } // class CustomerUseCases

} // namespace Empiria.Trade.Core.UsesCases
