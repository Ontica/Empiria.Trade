using System;


namespace Empiria.Trade.Sales {


  public class OrderActions {

    public OrderActions() {
    }

    public Boolean CanEdit {
      get; set;
    } = false;

    public Boolean CanApply {
      get; set;
    } = false;

    public Boolean CanAuthorize {
      get; set;
    } = false;

    public Boolean TransportPackaging {
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


    internal static OrderActions GetApplyActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = true;
      actions.CanEdit = false;
      actions.CanSelectCarrier = false;
      actions.TransportPackaging = false;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    internal static OrderActions GetAuthorizedActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = false;
      actions.CanEdit = false;
      actions.TransportPackaging = true;
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
      actions.TransportPackaging = false;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    internal static OrderActions GetPackingActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = false;
      actions.CanEdit = false;
      actions.TransportPackaging = false;
      actions.CanSelectCarrier = true;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

  }  //  class OrderActionsDto
}