/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : SimpleObjects                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a simple object.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Core.Catalogues {


  /// <summary>Represents a simple object.</summary>
  public class SimpleObjects {


    #region Public methods


    public FixedList<SimpleObjectData> GetParcelSupplierList() {

      return GetSimpleObjectDataList(1063);

    }


    public FixedList<SimpleObjectData> GetSimpleObjectDataList(int objectTypeId) {
      var data = new CataloguesData();

      var simpleObjectList = data.GetSimpleObjectDataList(objectTypeId);

      return simpleObjectList;
    }


    public FixedList<INamedEntity> MergeSimpleObjectToNamedEntityDto(FixedList<SimpleObjectData> simpleObjectList) {

      var returnedNamed = new List<INamedEntity>();

      foreach (var simpleObject in simpleObjectList) {

        var namedDto = new NamedEntity(simpleObject.ObjectKey, simpleObject.Name);

        returnedNamed.Add(namedDto);
      }

      return returnedNamed.ToFixedList();
    }


    static public string ConcatIntListIntoString(List<int> intList) {

      if (intList.Count == 0) {
        return string.Empty;
      }

      string stringList = "";
      foreach (var id in intList) {
        stringList += $"{id},";
      }

      return stringList.Remove(stringList.Length - 1, 1);
    }

    #endregion Public methods


    #region Private methods




    #endregion Private methods


  } // class SimpleObjects

} // namespace Empiria.Trade.Core.Catalogues
