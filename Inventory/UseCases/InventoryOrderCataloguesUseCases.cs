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


    public FixedList<NamedEntityDto> GetInventoryOrderTypes() {
      //TODO REGISTRAR DATOS EN BD. (ESTO ES PROVISIONAL)
      var type1 = new NamedEntityDto("5851e71b-3a1f-40ab-836f-ac3d2c9408de", "Orden de conteo físico inicial");
      var type2 = new NamedEntityDto("ab8e950e-94e9-4ae5-943a-49abad514g52", "Orden de conteo físico mensual");
      var type3 = new NamedEntityDto("wered868-a7ec-47f5-b1b9-8c0f73b04kuk", "Orden de conteo físico anual");
      var type4 = new NamedEntityDto("2vgf36bc-535c-4a07-8475-3e6568ebbopi", "Orden de traspaso");

      return new List<NamedEntityDto> { type1, type2, type3, type4 }.ToFixedList();
    }





    #endregion Public methods

  } // class InventoryOrderCataloguesUseCases

} // namespace Empiria.Trade.Inventory.UseCases
