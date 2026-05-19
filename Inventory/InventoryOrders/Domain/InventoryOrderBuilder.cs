/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryOrderBuilder                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Inventory order.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using System.Reflection;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Core.Inventories.Adapters;
using Empiria.Trade.Core.UsesCases;
using System.Security.Cryptography;

namespace Empiria.Trade.Inventory.Domain {

  /// <summary>Generate data for Inventory order.</summary>
  internal class InventoryOrderBuilder {


    #region Constructors and parsers

    public InventoryOrderBuilder() {

    }


    #endregion Constructor and parsers


    #region Public methods

    #endregion Public methods

    #region Private methods

    internal InventoryOrderFields MapToInventoryOrderFields(InventoryItems inventoryItemData) {

      InventoryOrderFields fields = new InventoryOrderFields();

      fields.InventoryOrderTypeUID = InventoryOrderType.Parse(504).UID; // TODO cambiar metodo de referencia
      fields.ResponsibleUID = PartyUseCases.GetWarehouseResponsible().FirstOrDefault().UID;
      fields.AssignedToUID = "";
      fields.Notes = "";
      fields.ReferenceId = inventoryItemData.OrderId;

      return fields;
    }



    internal InventoryOrderItemFields MapToInventoryOrderItemFields(
      InventoryItems inventoryItem, int InventoryOrderTypeId) {

      InventoryOrderItemFields fields = new InventoryOrderItemFields();
      fields.InventoryOrderTypeItemId = InventoryOrderTypeId;
      fields.ItemReferenceId = inventoryItem.OrderItemId;
      fields.VendorProductUID = inventoryItem.VendorProductUID;
      fields.WarehouseBinUID = inventoryItem.WarehouseBinUID;
      fields.InProcessOutputQuantity = inventoryItem.Quantity;
      fields.Notes = "APARTADO";
      return fields;
    }


    #endregion Private methods


  } // class InventoryOrderBuilder

} // namespace Empiria.Trade.Inventory.Domain
