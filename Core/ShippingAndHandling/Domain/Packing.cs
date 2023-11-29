/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : Packing                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a product attributes list.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;

namespace Empiria.Trade.ShippingAndHandling {


  public class Packing {


    [DataField("OrderPackingId")]
    public int OrderPackingId {
      get; private set;
    }


    [DataField("OrderPackingUID")]
    public string OrderPackingUID {
      get; private set;
    }


    [DataField("PackingItemId")]
    public int PackingItemId {
      get; private set;
    }


    [DataField("OrderId")]
    public Order Order {
      get; private set;
    }


    [DataField("OrderItemId")]
    public int OrderItemId {
      get; private set;
    }


    [DataField("InventoryEntryId")]
    public InventoryEntry InventoryEntry {
      get; private set;
    }


    [DataField("PackageID")]
    public string PackageID {
      get; private set;
    }


    [DataField("Size")]
    public string Size {
      get; private set;
    }


    [DataField("PackageQuantity")]
    public decimal PackageQuantity {
      get; private set;
    }


  } // class Packing


} // namespace Empiria.Trade.ShippingAndHandling
