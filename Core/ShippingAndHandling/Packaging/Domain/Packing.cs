/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : Packing                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represent a packing.                                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Core {


  public class Packing {

    [DataField("Order_Packing_Id")]
    public int OrderPackingId {
      get; private set;
    }


    [DataField("Order_Packing_UID")]
    public string OrderPackingUID {
      get; private set;
    }


    [DataField("Packing_Item_Id")]
    public int PackingItemId {
      get; private set;
    }


    [DataField("Packing_Item_UID")]
    public string PackingItemUID {
      get; private set;
    }


    [DataField("Package_Type_Id")]
    public PackageType PackageType {
      get; private set;
    }


    [DataField("Order_Id")]
    public SalesOrder Order {
      get; private set;
    }


    [DataField("Order_Item_Id")]
    public int OrderItemId {
      get; private set;
    }


    [DataField("Inventory_Entry_Id")]
    public int InventoryEntry {
      get; private set;
    }
    //public InventoryEntry InventoryEntry {
    //  get; private set;
    //}


    [DataField("Package_ID")]
    public string PackageID {
      get; private set;
    }


    [DataField("Package_Quantity")]
    public decimal Quantity {
      get; private set;
    }

  } // class Packing


} // namespace Empiria.Trade.ShippingAndHandling
