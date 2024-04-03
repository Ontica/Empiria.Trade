﻿/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Inventory.Data;

namespace Empiria.Trade.Inventory.Domain {

    /// <summary>Generate data for Inventory order.</summary>
    internal class InventoryOrderBuilder {


        #region Constructors and parsers

        public InventoryOrderBuilder() {

        }


        #endregion Constructor and parsers


        #region Public methods


        internal FixedList<InventoryOrderEntry> GetInventoryOrderList() {

            return InventoryOrderData.GetInventoryOrderList();
        }


        internal InventoryOrderEntry GetInventoryOrderByUID(string inventoryUID) {

            return InventoryOrderData.GetInventoryOrderByUID(inventoryUID).FirstOrDefault();
        }


        #endregion Public methods


    } // class InventoryOrderBuilder

} // namespace Empiria.Trade.Inventory.Domain
