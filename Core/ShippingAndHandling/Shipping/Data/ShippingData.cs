﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : ShippingAndHandlingData Management         Component : Data Layer                              *
*  Assembly : Empiria.Trade.ShippingAndHandlingData.dll  Pattern   : Data Service                            *
*  Type     : PackagingData                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write methods for shipping.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Core.ShippingAndHandling.Shipping.Domain;
using Empiria.Trade.Orders;

namespace Empiria.Trade.ShippingAndHandling.Data {

  /// <summary>Provides data read and write methods for shipping.</summary>
  internal class ShippingData {


    internal FixedList<SimpleDataObject> GetParcelSupplierList() {

      string sql = "SELECT * FROM SimpleObjects WHERE ObjectStatus = 'A' AND ObjectTypeId = 1063";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SimpleDataObject>(dataOperation);
    }


    internal ShippingEntry GetShippingForOrder(string orderUID) {
      
      int orderId = Order.Parse(orderUID).Id;

      string sql = $"SELECT * FROM TRDShipping WHERE OrderId = {orderId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObject<ShippingEntry>(dataOperation);

    }

  } // class ShippingData

} // namespace Empiria.Trade.ShippingAndHandling.Data
