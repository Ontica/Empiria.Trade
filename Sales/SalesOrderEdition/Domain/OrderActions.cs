/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Actions Management             Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : SalesOrderActions                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a sales order actions.                                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


namespace Empiria.Trade.Sales {

  /// <summary>Represents a sales order actions. </summary>
  public class OrderActions {

    #region Constructors and parsers

    public OrderActions() {
    }

    #endregion Constructors and parsers


    #region Public properties

    public Boolean CanEdit {
      get; set;
    } = false;

    public Boolean CanApply {
      get; set;
    } = false;

    public Boolean CanAuthorize {
      get; set;
    } = false;

    public Boolean CanPackaging {
      get; set;
    } = false;

    public Boolean CanSelectCarrier {
      get; set;
    } = false;

    public Boolean CanShipping {
      get; set;
    } = false;

    public Boolean CanClose {
      get; set;
    } = false;

    #endregion Public properties

    #region Internal methods

    internal static OrderActions GetApplyActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = true;
      actions.CanEdit = false;
      actions.CanSelectCarrier = false;
      actions.CanPackaging = false;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    internal static OrderActions GetAuthorizedActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = false;
      actions.CanEdit = false;
      actions.CanPackaging = true;
      actions.CanSelectCarrier = false;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    internal static OrderActions GetCancellActions() {
      OrderActions actions = new OrderActions();
      return actions;
    }

    internal static OrderActions GetCaptureActions() {
      OrderActions actions = new OrderActions();
      actions.CanApply = true;
      actions.CanAuthorize = false;
      actions.CanEdit = true;
      actions.CanSelectCarrier = false;
      actions.CanPackaging = false;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    internal static OrderActions GetPackingActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = false;
      actions.CanEdit = false;
      actions.CanPackaging = false;
      actions.CanSelectCarrier = true;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    internal static OrderActions GetSelectCarrierActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = false;
      actions.CanEdit = false;
      actions.CanPackaging = false;
      actions.CanSelectCarrier = false;
      actions.CanShipping = true;
      actions.CanClose = false;

      return actions;
    }

    internal static OrderActions GetSelectDeliverActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = false;
      actions.CanEdit = false;
      actions.CanPackaging = false;
      actions.CanSelectCarrier = false;
      actions.CanShipping = false;
      actions.CanClose = true;

      return actions;
    }

    #endregion Internal methods

  }  //  class OrderActionsDto

} // namespace Empiria.Trade.Sales

