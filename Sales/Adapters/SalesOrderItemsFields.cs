﻿/* Empiria Trade *********************************************************************************************
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


    public string OrderItemUID {
      get; set;
    } = string.Empty;

    public string VendorProductUID {
      get; set;
    }

    public int Quantity {
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
    } = 0;

    public decimal AdditionalDiscountToApply {
      get; set;
    } = 0;
        
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

  }  // class OrderItemsFields
 
} // namespace Empiria.Trade.Sales.Adapters
