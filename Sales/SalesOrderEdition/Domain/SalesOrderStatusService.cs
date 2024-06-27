/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Status Management              Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : SalesOrderStatusService                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Expose a sales order status.                                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Collections.Generic;

namespace Empiria.Trade.Sales {
  /// <summary>Expose a sales order status. </summary>
  internal static class SalesOrderStatusService {


    #region Internal Methods

    internal static FixedList<NamedEntityDto> GetStatusList() {

      var captured = new NamedEntityDto("Captured", "Capturada");
      var applied = new NamedEntityDto("Applied", "Aplicada");
      var authorized = new NamedEntityDto("Authorized", "Autorizada");
      var packing = new NamedEntityDto("Packing", "Surtiéndose");
      var shipping = new NamedEntityDto("Shipping", "Envío");
      var delivery = new NamedEntityDto("Delivery", "Entrega");
      var closed = new NamedEntityDto("Closed", "Cerrada");
      var cancelled = new NamedEntityDto("Cancelled", "Cancelada");

      List<NamedEntityDto> orderSalesStatus = new List<NamedEntityDto>();
      orderSalesStatus.Add(captured);
      orderSalesStatus.Add(applied);
      orderSalesStatus.Add(authorized);
      orderSalesStatus.Add(packing);
      orderSalesStatus.Add(shipping);
      orderSalesStatus.Add(delivery);
      orderSalesStatus.Add(closed);
      orderSalesStatus.Add(cancelled);

      return orderSalesStatus.ToFixedList<NamedEntityDto>();
    }

    internal static FixedList<NamedEntityDto> GetAuthorizationStatusList() {
      var authorized = new NamedEntityDto("authorized", "Autorizado");
      var pending = new NamedEntityDto("pending", "Por Autorizar");

      List<NamedEntityDto> orderSalesStatus = new List<NamedEntityDto>();

      orderSalesStatus.Add(authorized);
      orderSalesStatus.Add(pending);


      return orderSalesStatus.ToFixedList<NamedEntityDto>();
    }

    internal static FixedList<NamedEntityDto> GetPackingStatusList() {
      var toSupply = new NamedEntityDto("ToSupply", "Por surtir");
      var inprogress = new NamedEntityDto("InProgress", "En proceso");
      var supplied = new NamedEntityDto("Suppled", "Surtido");
      List<NamedEntityDto> orderPackcingStatusList = new List<NamedEntityDto>();

      orderPackcingStatusList.Add(toSupply);
      orderPackcingStatusList.Add(inprogress);
      orderPackcingStatusList.Add(supplied);

      return orderPackcingStatusList.ToFixedList<NamedEntityDto>();
    }

    #endregion Internal Methods

  } // class SalesStatusService

} // namespace Empiria.Trade.Sales
