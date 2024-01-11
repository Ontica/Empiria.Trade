/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : ShippingBuilder                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Shipping.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.Trade.Core.Common;
using Empiria.Trade.ShippingAndHandling.Data;

namespace Empiria.Trade.ShippingAndHandling.Domain {

  /// <summary>Generate data for Shipping.</summary>
  internal class ShippingBuilder {



    #region Constructor

    public ShippingBuilder() {

    }


    #endregion Constructor


    #region Public methods


    internal ShippingEntry CreateShippingForOrder() {

      var data = new ShippingData();

      throw new NotImplementedException();
    }


    internal FixedList<INamedEntity> GetParcelSupplierList() {
      var data = new ShippingData();

      var parcelSupplierList = data.GetParcelSupplierList();

      return MergeParcelToNamedDto(parcelSupplierList);
    }


    internal ShippingEntry GetShippingForOrder(string orderUID) {
      
      var data = new ShippingData();

      var shippingList = data.GetShippingForOrder(orderUID);

      if (shippingList.Count>0) {
        return shippingList.FirstOrDefault();
      } else {
        return new ShippingEntry();
      }

    }


    #endregion Public methods


    #region Private methods


    private FixedList<INamedEntity> MergeParcelToNamedDto(FixedList<SimpleDataObject> parcelSupplierList) {

      var returnedNamed = new List<INamedEntity>();

      foreach (var parcel in parcelSupplierList) {

        var namedDto = new NamedEntity(parcel.ObjectKey, parcel.Name);

        returnedNamed.Add(namedDto);
      }

      return returnedNamed.ToFixedList();
    }


    #endregion Private methods

  } // class ShippingBuilder
}
