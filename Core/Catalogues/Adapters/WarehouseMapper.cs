/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : WarehouseMapper                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map warehouses.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Trade.Core.Catalogues.Adapters {

  /// <summary>Methods used to map warehouses.</summary>
  public class WarehouseMapper {


    #region Public methods


    internal static FixedList<NamedEntityDto> MapToDto(FixedList<WarehouseBin> warehouseBins) {

      var mappedList = warehouseBins.Select((x) => MapToNamedEntity(x));

      return new FixedList<NamedEntityDto>(mappedList);
    }

    
    internal static FixedList<WarehouseBinForInventoryDto> MapWarehouseBins(
      FixedList<WarehouseBin> warehouseBins) {

      var mappedList = warehouseBins.Select((x) => MapWarehouseBinList(x));

      return new FixedList<WarehouseBinForInventoryDto>(mappedList);
    }


    #endregion Public methods


    private static NamedEntityDto MapToNamedEntity(WarehouseBin x) {

      return new NamedEntityDto(x.WarehouseBinUID, x.Tag);
    }


    private static WarehouseBinForInventoryDto MapWarehouseBinList(WarehouseBin x) {
      
      return new WarehouseBinForInventoryDto() {
        UID = x.WarehouseBinUID,
        Tag = x.Tag,
        Name = x.Name,
        WarehouseName = x.WarehouseName,
        RackRow = x.RackRow,
        RackColumn = x.RackColumn
      };
    }
  } // class WarehouseMapper

} // namespace Empiria.Trade.Core.Catalogues.Adapters
