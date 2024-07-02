/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : ReportDataDto                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return inventory reports data.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Inventory.Adapters {


  public interface IReportDto {

  } // interface IReportDto


  /// <summary>Output DTO used to return inventory reports data.</summary>
  public class ReportDataDto {


    public ReportQuery Query {
      get; set;
    }


    public FixedList<DataTableColumn> Columns {
      get; set;
    } = new FixedList<DataTableColumn>();


    public FixedList<IReportDto> Entries {
      get; set;
    } = new FixedList<IReportDto>();


  } // class ReportDataDto

} // namespace Empiria.Trade.Inventory.Adapters
