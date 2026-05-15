/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Information Holder                      *
*  Type     : InventoryOrder                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory stock by vendorProduct for sales.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Locations;
using Empiria.Orders;

using Empiria.Financial;

namespace Empiria.Trade.Core {

  /// <summary>Represents an inventory order.</summary>
  public class InventoryOrder : Order {

    #region Constructors and parsers

    protected InventoryOrder(OrderType orderType) : base(orderType) {
      //Requeired by Empiria Framework
    }


    public InventoryOrder(string warehouseUID, OrderType orderType) : base(orderType) {
      Assertion.Require(warehouseUID, nameof(warehouseUID));

      this.Warehouse = Location.Parse(warehouseUID);

      base.OrderNo = "INV-" + EmpiriaString.BuildRandomString(8)
                                  .ToUpperInvariant();
    }

    static public new InventoryOrder Parse(int id) => ParseId<InventoryOrder>(id);

    static public new InventoryOrder Parse(string uid) => ParseKey<InventoryOrder>(uid);

    static public new InventoryOrder Empty => ParseEmpty<InventoryOrder>();

    #endregion Constructors and parsers

    #region Properties

    public InventoryType InventoryType {
      get {
        return (InventoryType) base.Category;
      }
    }


    #endregion Properties

    #region Methods

    public virtual void AddItem(InventoryOrderItem orderItem) {
      Assertion.Require(orderItem, nameof(orderItem));

      base.Items.Add(orderItem);
    }


    public void CloseItems() {
      foreach (var item in base.GetItems<InventoryOrderItem>()) {
        item.Close();
        item.Save();
      }
    }


    internal protected new void Delete() {
      base.Delete();

      this.DeleteItems();
    }


    public override FixedList<IPayableEntity> GetPayableEntities() {
      return new FixedList<IPayableEntity>();
    }


    public virtual void RemoveItem(InventoryOrderItem orderItem) {
      Assertion.Require(orderItem, nameof(orderItem));

      base.Items.Remove(orderItem);
    }


    public void Update(InventoryOrderFields fields) {
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      fields.CategoryUID = fields.InventoryTypeUID;

      Warehouse = Patcher.Patch(fields.WarehouseUID, Warehouse);

      base.Update(fields);
    }

    #endregion Methods

    #region Helpers

    private void DeleteItems() {
      foreach (var item in base.GetItems<InventoryOrderItem>()) {
        Items.Remove(item);
        item.Save();
      }
    }

    #endregion Helpers

  } // class InventoryOrder

} // namespace Empiria.Inventory
