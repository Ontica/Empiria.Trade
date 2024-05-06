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


    internal static FixedList<WarehouseBinForInventoryDto> MapWarehouseBins(
      FixedList<WarehouseBin> warehouseBins) {

      var mappedList = warehouseBins.Select((x) => MapWarehouseBinList(x));

      return new FixedList<WarehouseBinForInventoryDto>(mappedList);
    }


    #endregion Public methods



    private static WarehouseBinForInventoryDto MapWarehouseBinList(WarehouseBin x) {
      
      return new WarehouseBinForInventoryDto() {
        UID = x.WarehouseBinUID,
        Name = x.BinDescription,
        Positions = x.Positions,
        WarehouseName = x.WarehouseName,
        Levels = x.Levels
      };
    }
  } // class WarehouseMapper

} // namespace Empiria.Trade.Core.Catalogues.Adapters
