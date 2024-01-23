/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : ParcelSupplier                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a parcel supplier.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Core.Catalogues {


  /// <summary>Represents a parcel supplier.</summary>
  public class ParcelSupplier {


    #region Public methods


    public FixedList<INamedEntity> GetParcelSupplierList() {
      var data = new CataloguesData();

      var parcelSupplierList = data.GetParcelSupplierList();

      return MergeParcelToNamedDto(parcelSupplierList);
    }


    #endregion Public methods


    #region Private methods


    private FixedList<INamedEntity> MergeParcelToNamedDto(FixedList<SimpleObjectData> parcelSupplierList) {

      var returnedNamed = new List<INamedEntity>();

      foreach (var parcel in parcelSupplierList) {

        var namedDto = new NamedEntity(parcel.ObjectKey, parcel.Name);

        returnedNamed.Add(namedDto);
      }

      return returnedNamed.ToFixedList();
    }


    #endregion Private methods


  } // class ParcelSupplier

} // namespace Empiria.Trade.Core.Catalogues
