/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackagingItem                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Packaging order item.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Linq;
using Empiria.Orders;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Core {

    /// <summary>Represents a Packaging order item.</summary>
    public class PackagingItem : BaseObject {



    #region Constructor and parsers


    public PackagingItem() {
      //no-op
    }

    static public PackagingItem Parse(int id) => ParseId<PackagingItem>(id);

    static public PackagingItem Parse(string uid) => ParseKey<PackagingItem>(uid);

    static public PackagingItem Empty => ParseEmpty<PackagingItem>();


    public PackagingItem(string orderUID, string orderPackingUID, MissingItemField missingItemFields) {

      Update(orderUID, orderPackingUID, missingItemFields);

    }

    #endregion Constructor and parsers


    #region Properties


    [DataField("Packing_Item_Id")]
    public int PackingItemId {
      get;
      internal set;
    }


    [DataField("Packing_Item_UID")]
    public string PackingItemUID {
      get;
      internal set;
    }


    [DataField("Order_Packing_Id")]
    public PackagingEntry OrderPacking {
      get;
      internal set;
    }


    [DataField("Order_Id")]
    public int OrderId {
      get;
      internal set;
    }


    [DataField("Order_Item_Id")]
    public int OrderItemId {
      get;
      internal set;
    }


    [DataField("Inventory_Entry_Id")]
    public int InventoryEntryId {
      get; private set;
    }


    [DataField("Warehouse_Bin_Id")]
    public int WarehouseBinId {
      get;
      internal set;
    }


    //[DataField("Warehouse_Bin_Id")]
    //public WarehouseBin WarehouseBin {
    //  get;
    //  internal set;
    //}


    [DataField("Package_Quantity")]
    public decimal Quantity {
      get;
      internal set;
    }


    #endregion Properties


    #region Private methods

    protected override void OnSave() {

      if (this.PackingItemId == 0) {
        this.PackingItemId = this.Id;
      }
    }


    public void Update(string orderUID, string orderPackingUID, MissingItemField missingItemFields) {

      var warehouseBin = WarehouseBin.Parse(missingItemFields.WarehouseBinUID);
      var orderItem = OrderItem.Parse(missingItemFields.orderItemUID);
      
      var existPackingItem = PackagingData.GetPackingOrderItem(
                              orderPackingUID, missingItemFields.orderItemUID,
                              warehouseBin.Id);
      
      if (existPackingItem.Count > 0) {
        this.PackingItemId = existPackingItem.First().PackingItemId;
        this.Quantity = existPackingItem.First().Quantity + missingItemFields.Quantity;

      } else {

        this.Quantity = missingItemFields.Quantity;
      }
      
      this.OrderPacking = PackagingEntry.Parse(orderPackingUID);
      this.OrderId = orderItem.Order.Id;
      this.OrderItemId = orderItem.Id;
      this.WarehouseBinId = warehouseBin.Id;
    }


    #endregion




  } // class PackingOrderItem

} // namespace Empiria.Trade.ShippingAndHandling
