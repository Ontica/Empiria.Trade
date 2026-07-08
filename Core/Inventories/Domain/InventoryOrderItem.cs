/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Material Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Information Holder                      *
*  Type     : InventoryOrderItem                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Inventory.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Locations;
using Empiria.Orders;
using Empiria.Trade.Products;

namespace Empiria.Trade.Core {

  /// <summary>Represents an inventory order item.</summary>
  public class InventoryOrderItem : OrderItem {

    #region Constructors and parsers

    protected InventoryOrderItem(OrderItemType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    protected internal InventoryOrderItem(OrderItemType powertype,
                                          InventoryOrder order) : base(powertype, order) {
    }

    public InventoryOrderItem(OrderItemType powertype,
                                          InventoryOrder order, Location location) : base(powertype, order) {

      Assertion.Require(location, nameof(location));
      Assertion.Require(!location.IsEmptyInstance, nameof(location));

      base.Location = location;
    }


    static public new InventoryOrderItem Parse(int id) => ParseId<InventoryOrderItem>(id);

    static public new InventoryOrderItem Parse(string uid) => ParseKey<InventoryOrderItem>(uid);

    static public InventoryOrderItem Empty => ParseEmpty<InventoryOrderItem>();

    #endregion Constructors and parsers

    #region Properties

    public FixedList<InventoryEntry> Entries {
      get; set;
    }


    public decimal PackagingSize {
      get {
        return ProductEntry.ParseUID(this.Product.UID).PackagingSize;
      }
    }


    public decimal PackingSmallBag {
      get {
        return ProductEntry.ParseUID(this.Product.UID).PackingSmallBag;
      }
    }

    #endregion Properties

    #region Methods

    public void GetProductByCode(string productCode) {
      this.Product = Empiria.Products.Product.TryParseWithCode(productCode);
    }


    public void Update(InventoryOrderItemFields fields) {
      Assertion.Require(fields, nameof(fields));

      GetProductByCode(fields.Product);

      fields.UnitPrice = GetProductPrice();

      fields.EnsureValid();

      base.Update(fields);
    }


    public new void UpdateQuantity(decimal quantity) {

      base.UpdateQuantity(quantity);
    }


    internal new void Close() {
      base.Close();
    }

    #endregion Methods

    #region Helpers

    private decimal GetProductPrice() {

      var unitPrice = InventoryData.GetProductPriceFromVirtualWarehouse(this.Product.Id);

      if (unitPrice == 0) {
        return InventoryData.GetProductPriceFromHistoricCost(this.Product.Name);
      } else {
        return unitPrice;
      }
    }

    #endregion Helpers

  } // class InventoryOrderItem

} // namespace Empiria.Trade.Core
