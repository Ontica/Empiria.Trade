/* Empiria Operations ****************************************************************************************
*                                                                                                            *
*  Module   : Material Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Inventory.Core.dll                 Pattern   : Information Holder                      *
*  Type     : InventoryEntryReport                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory report entry.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Trade.Inventory {
  /// <summary>Represents an inventory entry.</summary>
  internal class InventoryEntriesReport {

    #region Properties

    [DataField("PRODUCT_NAME")]
    public string ProductName {
      get; internal set;
    }


    [DataField("PRODUCT_DESCRIPTION")]
    public string ProductDescription {
      get; internal set;
    }


    [DataField("PRODUCT_ID")]
    public int ProductId {
      get; internal set;
    }


    [DataField("INV_ENTRY_ORDER_ID")]
    public int OrderId {
      get; internal set;
    }


    [DataField("SUM_INV")]
    public decimal InventoryCount {
      get; internal set;
    }


    [DataField("SALDO_INICIAL")]
    public decimal Stock {
      get; internal set;
    }


    [DataField("DIFERENCIA")]
    public decimal CountVariance {
      get; internal set;
    }


    [DataField("COSTO_INV_PROM")]
    public decimal AverageCost {
      get; internal set;
    }


    [DataField("VALOR_FINAL")]
    public decimal FinalCost {
      get; internal set;
    }


    [DataField("ESTADO_UBICACION")]
    public string IsMultiLocation {
      get; internal set;
    }


    [DataField("Location_Name_Readed")]
    public string LocationReaded {
      get; internal set;
    }


    [DataField("Locations_Names")]
    public string Locations {
      get; internal set;
    }

    #endregion Properties

  } // class InventoryEntriesReport

} // namespace Empiria.Inventory 

