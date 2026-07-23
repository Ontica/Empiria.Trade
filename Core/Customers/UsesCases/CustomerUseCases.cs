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

      Parties.Party customer = Parties.Party.Parse(customerUID);

      return CustomerAddressMapper.MapCustomerShortAddresses(CustomerAddress.GetAddresses(customer.Id));
    }


    public FixedList<CustomerContactDto> GetCustomerContacts(int customerId) {

      return CustomerConctacMapper.MapCustomerContacts(CustomerContact.GetContacts(customerId));
    }

    #endregion Use cases

  } // class CustomerUseCases

} // namespace Empiria.Trade.Core.UsesCases
