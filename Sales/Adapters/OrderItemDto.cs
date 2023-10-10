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
  public class OrderItemDto {

    #region Public properties


    public string UID {
      get; set;
    } = string.Empty;

    public decimal Quantity {
      get; set;
    }

    public decimal BasePrice {
      get; set;
    }

    public decimal SalesPrice {
      get; set;
    }

    public decimal AdditionalDiscount {
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

    public string ProductUID {
      get; set;
    }

    public string PresentationUID {
      get; set;
    }

    public string VendorUID {
      get; set;
    }

    public EntityStatus Status {
      get; set;
    }

    #endregion

  } //  public class OrderItemDto

} // namespace Empiria.Trade.Sales.Adapters
