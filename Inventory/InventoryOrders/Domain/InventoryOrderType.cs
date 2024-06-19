/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : InventoryOrderType                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory order type.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Inventory {


  /// <summary>Represents an inventory order type.</summary>
  public class InventoryOrderType : GeneralObject {


    #region Constructor and parsers


    public InventoryOrderType() {
      //no-op
    }

    static public InventoryOrderType Parse(int id) => ParseId<InventoryOrderType>(id);

    static public InventoryOrderType Parse(string uid) => ParseKey<InventoryOrderType>(uid);

    static public InventoryOrderType Empty => ParseEmpty<InventoryOrderType>();

    static public FixedList<InventoryOrderType> List() => GetList<InventoryOrderType>().ToFixedList();

    static public FixedList<NamedEntityDto> NamedEntityList() => GetList<InventoryOrderType>().MapToNamedEntityList();

    #endregion Constructor and parsers


    #region Properties


    [DataField("ObjectExtData")]
    public string ObjectExtData {
      get; set;
    }


    public NamedEntity InventoryOrderTypeCode => GetTypeCode();


    #endregion Properties


    #region Private methods

    public NamedEntity GetTypeCode() {

      var attr = new Attributes().GetAttributesList(ObjectExtData).FirstOrDefault();
      
      if (attr is null) {
        return new NamedEntity("", "");
      }
      return new NamedEntity(attr.Name, attr.Value);
    }
    

    #endregion


  } // class InventoryOrderType

} // namespace Empiria.Trade.Inventory
