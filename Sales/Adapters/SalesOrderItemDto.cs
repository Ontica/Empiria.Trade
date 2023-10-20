/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Data Transfer Object                    *
*  Type     : OrderItemDto                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return order items.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Output DTO used to return order items </summary>
  public class SalesOrderItemDto {

    #region Public properties


    public string OrderItemUID {
      get; set;
    } = string.Empty;

    public decimal Quantity {
      get; set;
    }
       
    public decimal BasePrice {
      get; set;
    }

    public decimal SpecialPrice {
      get; set;
    }

    public decimal SalesPrice {
      get; set;
    }

    public decimal AdditionalDiscount {
      get; set;
    }

    public decimal AdditionalDiscountToApply {
      get; set;
    } 

    public decimal Shipment {
      get; set;
    }

    public decimal Taxes {
      get; set;
    }

    public decimal Total {
      get; set;
    }

    public string Notes {
      get; set;
    }
    

    #endregion

  } //  public class OrderItemDto

} // namespace Empiria.Trade.Sales.Adapters
