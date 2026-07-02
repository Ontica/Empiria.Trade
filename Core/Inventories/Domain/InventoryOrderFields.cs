/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryOrderFields                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input fields DTO used to create and update inventory order.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Linq;
using Empiria.Orders;
using Empiria.Parties;

namespace Empiria.Trade.Core {

  public enum InventoryStatus {

    Todos = 'T',

    Abierto = 'A',

    EnProceso = 'E',

    Cerrado = 'C',

    Deleted = 'X'

  } // enum InventoryStatus


  public class InventoryOrderFields : OrderFields {

    public string WarehouseUID {
      get; set;
    } = string.Empty;


    public string InventoryTypeUID {
      get; set;
    } = string.Empty;


    public override void EnsureValid() {
      Assertion.Require(WarehouseUID, nameof(WarehouseUID));

      Assertion.Require(InventoryTypeUID, nameof(InventoryTypeUID));
      _ = InventoryType.Parse(InventoryTypeUID);

      Assertion.Require(ResponsibleUID, nameof(ResponsibleUID));
      _ = Parties.Party.Parse(ResponsibleUID);

      Assertion.Require(RequestedByUID, nameof(RequestedByUID));
      _ = Parties.Party.Parse(RequestedByUID);

      Assertion.Require(Description, nameof(Description));
    }

  } // class InventoryOrderFields


  public class InventoryEntryFields {

    public string UID {
      get; set;
    } = string.Empty;


    public string ProductUID {
      get; set;
    } = string.Empty;


    public string LocationUID {
      get; set;
    } = string.Empty;


    public string Location {
      get; set;
    } = string.Empty;


    public string Product {
      get; set;
    } = string.Empty;


    public decimal Quantity {
      get; set;
    }

    public decimal Cost {
      get; set;
    }

  } // class InventoryEntryFields


  static public class InventoryEntryFieldsExtensions {

    static public void EnsureIsValid(this InventoryEntryFields fields,
                                       int productId,
                                       string orderItemUID) {

      InventoryOrderItem orderItem = InventoryOrderItem.Parse(orderItemUID);
      var entries = InventoryEntry.GetListFor(orderItem);

      Assertion.Require((fields.Quantity + entries.Sum(x => x.InputQuantity)) <= orderItem.Quantity,
                        $"La cantidad de productos capturados supera los productos restantes.");

      Assertion.Require(productId == orderItem.Product.Id, "El producto no coincide con el seleccionado.");
    }
  } // class InventoryEntryFieldsExtensions

} // namespace Empiria.Trade.Core
