/* Empiria Operations ****************************************************************************************
*                                                                                                            *
*  Module   : Material Management                        Component : Adapters Layer                          *
*  Assembly : Empiria.Inventory.Core.dll                 Pattern   : Data Transfer Object                    *
*  Type     : InventoryOrderMapper                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Mapping methods for inventory order.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
namespace Empiria.Trade.Inventory.Adapters {

  ///<summary>Output DTO used to return inventory entries report</summary>
  public class InventoryEntryReportDto {

    public string ProductCode {
      get; internal set;
    }


    public string ProductDescription {
      get; internal set;
    }


    public decimal PhysicalCount {
      get; internal set;
    }


    public decimal Stock {
      get; internal set;
    }


    public decimal Variance {
      get; internal set;
    }

    public decimal CostVariance {
      get; internal set;
    }


    public string IsMultiLocation {
      get; internal set;
    }


    public string LocationReaded {
      get; internal set;
    }


    public string Locations {
      get; internal set;
    }

  }  // class InventoryEntryReportDto 

  /// <summary>Mapping methods for inventory order.</summary>
  static internal class InventoryEntriesReportMapper {

    #region Public methods

    static internal FixedList<InventoryEntryReportDto> Map(FixedList<InventoryEntriesReport> entries) {
      return entries.Select(x => Map(x))
                  .ToFixedList();
    }

    #endregion Public methods

    #region Helpers

    static private InventoryEntryReportDto Map(InventoryEntriesReport entry) {
      return new InventoryEntryReportDto {
        ProductCode = entry.ProductName,
        ProductDescription = entry.ProductDescription,
        PhysicalCount = entry.InventoryCount,
        Stock = entry.Stock,
        Variance = entry.CountVariance,
        CostVariance = entry.FinalCost,
        IsMultiLocation = entry.IsMultiLocation,
        LocationReaded = entry.LocationReaded,
        Locations = entry.Locations
      };

    }

    #endregion helpers

  } // class InventoryOrderMapper

} // namespace Empiria.Inventory.Adapters
