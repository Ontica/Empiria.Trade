﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : InventoryData                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for inventory order.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;
using Empiria.Trade.Core;
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.Data {


    /// <summary>Provides data read methods for inventory order.</summary>
    internal class InventoryOrderData {


        #region Public methods


        static internal FixedList<InventoryOrderEntry> GetInventoryOrderList() {

            string sql = $"SELECT * FROM TRDInventory ";

            var dataOperation = DataOperation.Parse(sql);

            return DataReader.GetPlainObjectFixedList<InventoryOrderEntry>(dataOperation);

        }


        static internal FixedList<InventoryOrderEntry> GetInventoryOrderByUID(string inventoryUID) {

            string sql = $"SELECT * FROM TRDInventory WHERE InventoryEntryUID IN ({inventoryUID})";

            var dataOperation = DataOperation.Parse(sql);

            return DataReader.GetPlainObjectFixedList<InventoryOrderEntry>(dataOperation);

        }


        #endregion Public methods


    } // class InventoryOrderData

} // namespace Empiria.Trade.Inventory.Data
