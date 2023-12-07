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

using Empiria.Trade.Products.Adapters;

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
       
    public decimal UnitPrice {
      get; set;
    }

    public decimal SalesPrice {
      get; set;
    }

    public string DiscountPolicy {
      get; set;
    }

    public decimal Discount1 {
      get; set;
    }

    public decimal Discount2 {
      get; set;
    }
       
    public decimal Subtotal {
      get; set;
    }

    public string Notes {
      get; set;
    } 

    public VendorDto Vendor {
      get; set;
    }

    public PresentationDto Presentation {
      get; set;
    }

    public BaseProductDto Product {
      get; set;
    }


    #endregion

  } //  public class OrderItemDto


 


} // namespace Empiria.Trade.Sales.Adapters
