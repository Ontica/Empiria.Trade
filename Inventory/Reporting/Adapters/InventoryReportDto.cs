/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : InventoryOrderDto                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return inventory report data.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Output DTO used to return inventory report data.</summary>
  public class InventoryReportDto {


  } // class InventoryReportDto


  /// <summary>Output DTO used to return inventory report descriptor data.</summary>
  internal class InventoryReportDescriptorDto : IInventoryReportDto {


  } // class InventoryReportDescriptorDto


  /// <summary>Output DTO used to return inventory stock descriptor data.</summary>
  internal class InventoryStockDescriptorDto : IInventoryReportDto {


  } // class InventoryStockDescriptorDto


} // namespace Empiria.Trade.Inventory.Adapters
