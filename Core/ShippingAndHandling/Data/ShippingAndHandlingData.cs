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

namespace Empiria.Trade.ShippingAndHandling.Data {


  /// <summary>Provides data read  and write methods for shipping and handling.</summary>
  internal class ShippingAndHandlingData {



    internal static void Write(PackagingOrder order) {


      var op = DataOperation.Parse("writePackaging", order.Id,
                                                     order.OrderId,
                                                     order.OrderItemId,
                                                     order.PackageQuantity,
                                                     order.PackageID);
      DataWriter.Execute(op);



    }



  } // class ShippingAndHandlingData

} // namespace Empiria.Trade.ShippingAndHandling.Data
