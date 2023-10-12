/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : OrderItemsFields                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a OrderItems properties.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;
using Empiria.Trade.Products;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Holds a OrderItems properties. </summary>
  public class SalesOrderItemsFields {

    #region Public properties


    public string UID {
      get; set;
    } = string.Empty;
        
    public int Quantity {
      get; set;
    }

    public int ProductPriceId {
      get; set;
    }

    public int PriceListNumber {
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
      get;  set;
    }

    public string VendorUID {
      get; set;
    }

    public EntityStatus Status {
      get; set;
    }

    #endregion

  }  // class OrderItemsFields

} // namespace Empiria.Trade.Sales.Adapters
