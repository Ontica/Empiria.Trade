/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : ShippingLabelBuilder                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Shipping labels.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Sales.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain {


  /// <summary>Generate data for Shipping labels.</summary>
  internal class ShippingLabelBuilder {


    #region Constructor

    public ShippingLabelBuilder() {

    }


    #endregion Constructor


    #region Public methods


    internal FixedList<ShippingLabel> GetShippingLabels(string shippingUID) {

      FixedList<ShippingOrderItem> ordersForShipping =
        ShippingData.GetOrdersForShippingByShippingId(shippingUID);

      var shippingLabels = new List<ShippingLabel>();

      foreach (var orderForShipping in ordersForShipping) {

        FixedList<SalesOrderItem> orderItems = SalesOrderItemsData.GetOrderItems(orderForShipping.Order.Id);

        shippingLabels.AddRange(MapToShippingLabelByOrderItem(orderItems));
      }

      return shippingLabels.ToFixedList();
    }


    #endregion Public methods


    #region Private methods


    private List<ShippingLabel> MapToShippingLabelByOrderItem(FixedList<SalesOrderItem> orderItems) {

      var shippingLabels= new List<ShippingLabel>();

      foreach (var item in orderItems) {
        
        var warehouse = 
          CataloguesUseCases.GetWarehouseBinProductByVendorProduct(item.VendorProduct.Id);
        warehouse.GetDescription();

        var shippingLabel = new ShippingLabel {
          OrderUID = item.Order.UID,
          ProductCode = item.VendorProduct.ProductFields.ProductCode,
          ProductPresentation = item.VendorProduct.ProductPresentation.PresentationName,
          Description = item.VendorProduct.ProductFields.ProductDescription,
          Comments = "",
          Quantity = item.Quantity,
          Ubication = warehouse.Description
        };

        shippingLabels.Add(shippingLabel);
      }

      return shippingLabels;
    }


    #endregion Private methods

  } // class ShippingLabelBuilder

} // Empiria.Trade.Sales.ShippingAndHandling.Domain
