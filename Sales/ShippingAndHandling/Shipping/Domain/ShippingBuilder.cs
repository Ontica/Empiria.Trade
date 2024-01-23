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
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain {

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


    internal ShippingEntry GetShippingForOrder(string orderUID) {
      
      var shippingList = ShippingData.GetShippingForOrder(orderUID);

      if (shippingList.Count>0) {
        return shippingList.FirstOrDefault();
      } else {
        return new ShippingEntry();
      }

    }


    #endregion Public methods


  } // class ShippingBuilder
}
