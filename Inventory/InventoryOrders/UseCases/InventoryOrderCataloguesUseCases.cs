/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use case interactor class               *
*  Type     : InventoryOrderCataloguesUseCases           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build Inventory order catalogues.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Services;

namespace Empiria.Trade.Inventory.UseCases {

  /// <summary>Use cases used to build Inventory order catalogues.</summary>
  public class InventoryOrderCataloguesUseCases : UseCase {


    #region Constructors and parsers

    public InventoryOrderCataloguesUseCases() {
      // no-op
    }

    static public InventoryOrderCataloguesUseCases UseCaseInteractor() {
      return CreateInstance<InventoryOrderCataloguesUseCases>();
    }


    #endregion Constructors and parsers

    #region Public methods


    public InventoryOrderType GetInventoryOrderTypeByUID(string typeUID) {
      
      var inventoryOrderType = InventoryOrderType.Parse(typeUID);

      return inventoryOrderType;
    }


    public FixedList<NamedEntityDto> GetInventoryOrderTypesNamedEntity() {

      return InventoryOrderType.NamedEntityList();
    }


    public FixedList<InventoryOrderType> GetInventoryOrderTypeList() {

      return InventoryOrderType.List();
    }


    #endregion Public methods

  } // class InventoryOrderCataloguesUseCases

} // namespace Empiria.Trade.Inventory.UseCases
