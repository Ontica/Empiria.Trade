/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : OrderShippingBuilder                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for shipping and handling.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Trade.Orders;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.ShippingAndHandling.Data;
using Newtonsoft.Json;

namespace Empiria.Trade.ShippingAndHandling.Domain {


  /// <summary>Generate data for shipping and handling.</summary>
  internal class ShippingAndHandlingBuilder {


    #region Constructor

    public ShippingAndHandlingBuilder() {

    }


    #endregion Constructor


    #region Public methods


    internal FixedList<INamedEntity> GetPackageTypeList() {
      var data = new ShippingAndHandlingData();

      FixedList<PackageType> packageTypes = data.GetPackageTypeList();

      GetVolumeAttributes(packageTypes);

      GetTotalVolume(packageTypes);

      FixedList<INamedEntity> namedDto = MergePackageTypeToNamedDto(packageTypes);

      return namedDto;
    }


    internal FixedList<Packing> GetPackingByOrder(string orderUid) {

      int orderId = Order.Parse(orderUid).Id;

      var data = new ShippingAndHandlingData();

      var packingList = data.GetPackingByOrder(orderId);

      return packingList;
    }


    #endregion Public methods


    #region Private methods


    private void GetTotalVolume(FixedList<PackageType> packageTypes) {

      foreach (var package in packageTypes) {
        package.GetTotalVolume();
      }

    }


    internal void GetVolumeAttributes(FixedList<PackageType> packageTypes) {

      foreach (var packageType in packageTypes) {

        packageType.Attributes = packageType.GetExtData();

        foreach (var attr in packageType.Attributes) {

          decimal value = Convert.ToDecimal(attr.Value);

          if (attr.Name == "length") {
            packageType.Length = value;
          }

          if (attr.Name == "width") {
            packageType.Width = value;
          }

          if (attr.Name == "height") {
            packageType.Height = value;
          }

        }
      }
    }


    private FixedList<INamedEntity> MergePackageTypeToNamedDto(FixedList<PackageType> packageTypes) {

      var returnedNamed = new List<INamedEntity>();

      foreach (var package in packageTypes) {

        var packageName = $"{package.Name} " +
                          $"({package.Length.ToString()}x" +
                          $"{package.Width.ToString()}x" +
                          $"{package.Height.ToString()})";

        var namedDto = new NamedEntityDto(package.ObjectKey, packageName);

        returnedNamed.Add(namedDto);
      }

      return returnedNamed.ToFixedList();
    }

    
    #endregion Private methods

  } // class ShippingAndHandlingBuilder

} // namespace Empiria.Trade.ShippingAndHandling.Domain
