/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : OrderMapper                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Order.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Sales.Domain;
using Empiria.Trade.Sales.Adapters;

using Empiria.Trade.Core.Adapters;
using Empiria.Trade.Core.UsesCases;


namespace Empiria.Trade.Sales.Adapters {

  /// <summary> Methods used to map Order. </summary>
  static internal class OrderMapper {

    static internal OrderDto Map(Order order) {
      var dto = new OrderDto {
        OrderUID = order.OrderUID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        Notes = order.Notes,
        Status = "Abierto",
        Customer = GetCustomer(order.CustomerId),
        Supplier = GetSupplier(order.SupplierId),
        SalesAgent = GetSalesAgent(order.SalesAgentId),
        PaymentCondition = "30 dias"
      };

      return dto;
    }

    static private ShortPartyDto GetParty(int partyId) {
      var usecase = PartyUseCases.UseCaseInteractor();

      return usecase.GetParty(partyId);
    }

    static private CustomerDto GetCustomer(int customerId) {
    
      var customer = GetParty(customerId);
      var dto = new CustomerDto {
        UID = customer.UID,
        Name = customer.Name
      };

      return dto;
    }

    static private SupplierDto GetSupplier(int supplierId) {

      var suppplier = GetParty(supplierId);

      var dto = new SupplierDto {
        UID = suppplier.UID,
        Name = suppplier.Name
      };

      return dto;
    }

    static private SalesAgentDto GetSalesAgent(int saleAgentId) {

      var saleAgent = GetParty(saleAgentId);

      var dto = new SalesAgentDto {
        UID = saleAgent.UID,
        Name = saleAgent.Name
      };

      return dto;
    }

  } // static internal class

  

} // namespace Empiria.Trade.Sales.Adapters
