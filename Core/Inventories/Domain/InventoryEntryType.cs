/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Material Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Power type                              *
*  Type     : InventoryEntryType                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Power type that describes an inventory entry.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Linq;
using Empiria.Ontology;
using Empiria.Orders;

namespace Empiria.Trade.Core {

  /// <summary></summary>
  [Powertype(typeof(InventoryEntry))]
  public sealed class InventoryEntryType : Powertype {

    #region Constructors and parsers

    private InventoryEntryType() {
      // Empiria powertype types always have this constructor.
    }

    static public InventoryEntryType Empty => Parse("ObjectTypeInfo.InventoryEntry");

    static public new InventoryEntryType Parse(int typeId) => Parse<InventoryEntryType>(typeId);

    static public new InventoryEntryType Parse(string typeName) => Parse<InventoryEntryType>(typeName);

    static public FixedList<InventoryEntryType> GetList() {
      return Empty.GetAllSubclasses()
                  .Select(x => (InventoryEntryType) x)
                  .ToFixedList();
    }

    static public InventoryEntryType InventoryEntryItemType => Parse("ObjectTypeInfo.InventoryEntry");

    #endregion Constructors and parsers

  } // class InventoryEntryType

} // namespace Empiria.Trade.Core
