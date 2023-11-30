/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : ShippingAndHandlingData Management         Component : Data Layer                              *
*  Assembly : Empiria.Trade.ShippingAndHandlingData.dll  Pattern   : Data Service                            *
*  Type     : ShippingAndHandlingData                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read  and write methods for shipping and handling.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;
using Empiria.Trade.Products.Adapters;
using Newtonsoft.Json;

namespace Empiria.Trade.ShippingAndHandling.Data {


  /// <summary>Provides data read  and write methods for shipping and handling.</summary>
  internal class ShippingAndHandlingData {


    internal FixedList<Packing> GetPackingByOrder(string orderUid) {
      
      int orderId = Order.Parse(orderUid).Id;

      string sql = "SELECT PACK.OrderPackingId, PACK.OrderPackingUID, ITEM.PackingItemId, " +
                   "ITEM.PackingItemUID, PACK.OrderId, ITEM.OrderItemId, PACK.PackageTypeId, " +
                   "ITEM.InventoryEntryId, PACK.PackageID, PACK.Size, ITEM.PackageQuantity " +
                   "FROM TRDPackaging PACK " +
                   "INNER JOIN TRDPackagingItems ITEM ON PACK.OrderPackingId = ITEM.OrderPackingId " +
                   $"WHERE PACK.OrderId IN ({orderId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<Packing>(dataOperation);

    }


    internal static void Write(PackingItem order) {


      var op = DataOperation.Parse("writePackaging", order.Id,
                                                     order.OrderPackingUID,
                                                     order.OrderId,
                                                     order.PackageID,
                                                     order.Size);
      DataWriter.Execute(op);



    }


    internal FixedList<PackageType> GetPackageTypeList() {
      
      string sql = "SELECT * FROM SimpleObjects WHERE ObjectStatus = 'A' AND ObjectTypeId = 1061";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PackageType>(dataOperation);
    }


    #region Private methods


    #endregion Private methods

  } // class ShippingAndHandlingData

} // namespace Empiria.Trade.ShippingAndHandling.Data
