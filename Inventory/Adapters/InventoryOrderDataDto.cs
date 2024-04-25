/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : InventoryOrderDescriptorDto                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return inventory descriptor data.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Inventory.Adapters {


  public interface IInventoryOrderDto {

  }


  public class InventoryOrderDataDto {

    public InventoryOrderQuery Query {
      get; set;
    }


    public FixedList<DataTableColumn> Columns {
      get; set;
    } = new FixedList<DataTableColumn>();


    public FixedList<IInventoryOrderDto> Entries {
      get; set;
    } = new FixedList<IInventoryOrderDto>();

  }


} // namespace Empiria.Trade.Inventory.Adapters
