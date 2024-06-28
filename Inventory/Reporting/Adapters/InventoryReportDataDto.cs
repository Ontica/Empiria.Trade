/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : InventoryReportDto                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return inventory reports data.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Inventory.Adapters {


  public interface IInventoryReportDto {

  } // interface IInventoryReportDto


  /// <summary>Output DTO used to return inventory reports data.</summary>
  public class InventoryReportDataDto {


    public ReportQuery Query {
      get; set;
    }


    public FixedList<DataTableColumn> Columns {
      get; set;
    } = new FixedList<DataTableColumn>();


    public FixedList<IInventoryReportDto> Entries {
      get; set;
    } = new FixedList<IInventoryReportDto>();


  } // class InventoryReportDataDto

} // namespace Empiria.Trade.Inventory.Adapters
