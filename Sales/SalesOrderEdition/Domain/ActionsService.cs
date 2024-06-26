﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Actions Management             Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : SalesOrderActions                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a sales order transaction actions.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using static Empiria.Trade.Sales.Adapters.SalesOrderFields;

namespace Empiria.Trade.Sales {

  /// <summary>Represents a sales order actions service. </summary>
   public  class ActionsService {

    #region Constructors and parsers

    public ActionsService() {

    }

    static public ActionsService Load() {
      return new ActionsService();
    }

    #endregion Constructors and parsers

    #region Public Properties

    public bool OnApplyEvent {
      get; private set;
    } = false;

    public bool OnAuthorizeEvent {
      get; private set;
    } = false;

    public bool OnSupplyEvent {
      get; private set;
    } = false;
    
   public bool OnCreateEvent {
      get; private set;
    } = false;

    #endregion Public Properties

    #region Public Methods

    public void OnCreate() => OnCreateEvent = true;

    public void OnApply() => OnApplyEvent = true;

    public void OnAuthorize() => OnAuthorizeEvent = true;

    public void OnSuppy() => OnSupplyEvent = true;

     public TransactionActions SetActions(SalesOrder salesOrder, QueryType queryType) {

      TransactionActions Actions = new TransactionActions();

      Actions.Show.OrderData = ValidateShowOrderData(queryType);
      Actions.Show.CreditData = ValidateShowCreditEdtior(queryType);
      Actions.Show.PickingData = ValidateShowPickingEditor(queryType);
      Actions.Show.PackingData = ValidateShowPackingEditor(queryType);
      Actions.Show.ShippingData = ValidateShippingEditor(queryType);
      Actions.Show.SendShippingData = ValidateSendShippingEditor(queryType);


      Actions.Can.Apply = ValidateApply(queryType, salesOrder);
      Actions.Can.Update = ValidateUpdate(queryType, salesOrder);
      Actions.Can.Cancel = ValidateCancel(queryType, salesOrder);
      Actions.Can.Authorize = ValidateAuthorize(queryType, salesOrder);
      Actions.Can.Deauthorize = ValidateDeauthorize(queryType, salesOrder);
      Actions.Can.EditPicking = ValidateEditPicking(queryType, salesOrder);
      Actions.Can.EditPacking = ValidateEditPacking(queryType, salesOrder);
      Actions.Can.ClosePacking = ValidateEditClosePacking(queryType, salesOrder);
      Actions.Can.EditShipping = ValidateEditShipping(salesOrder);
      Actions.Can.SendShipping = ValidateEditSendShipping(salesOrder);

      return Actions;
    }

    #endregion Public Methods

    #region Private Methods

    private bool ValidateCancel(QueryType queryType, SalesOrder salesOrder) {
      return (salesOrder.Status == OrderStatus.Captured && queryType == QueryType.Sales) || OnCreateEvent;
    }

    private bool ValidateApply(QueryType queryType, SalesOrder salesOrder) {
      return (salesOrder.Status == OrderStatus.Captured && queryType == QueryType.Sales && !OnApplyEvent) || OnCreateEvent;
      
    }

    private bool ValidateUpdate(QueryType queryType, SalesOrder salesOrder) {
      return (salesOrder.Status == OrderStatus.Captured && queryType == QueryType.Sales) || OnCreateEvent;
    }

    private bool ValidateAuthorize(QueryType queryType, SalesOrder salesOrder) {
      return salesOrder.Status == OrderStatus.Applied && queryType == QueryType.SalesAuthorization && !OnAuthorizeEvent;
    }

    private bool ValidateDeauthorize(QueryType queryType, SalesOrder salesOrder) {
      return salesOrder.AuthorizationStatus == OrderAuthorizationStatus.Authorized && queryType == QueryType.SalesAuthorization;
    }

    private bool ValidateEditPacking(QueryType queryType, SalesOrder salesOrder) {

      if (salesOrder.Status != OrderStatus.Packing) {
        return false;
      }

      if (queryType != QueryType.SalesPacking) {
        return false;
      }
          

      if (OnSupplyEvent) {
        return false;
      }

      return true;

    }

    private bool ValidateEditPicking(QueryType queryType, SalesOrder salesOrder) {

      if (salesOrder.Status != OrderStatus.Packing) {
        return false;
      }

      if (queryType != QueryType.SalesPacking) {
        return false;
      }


      if (OnSupplyEvent) {
        return false;
      }

      return true;

    }

    private bool ValidateEditClosePacking(QueryType queryType, SalesOrder salesOrder) {

      if (salesOrder.Status != OrderStatus.Packing) {
        return false;
      }

      if (queryType != QueryType.SalesPacking) {
        return false;
      }

      if (OnSupplyEvent) {
        return false;
      }

      var packingUseCase = PackagingUseCases.UseCaseInteractor();
      var packingOrder = packingUseCase.GetPackagingForOrder(salesOrder.UID);

      if (packingOrder.MissingItems.Count == 0)  {
        return true;
      } else {
        return false;
      }

    }

    private bool ValidateEditShipping(SalesOrder salesOrder) {
      return salesOrder.Status == OrderStatus.Shipping;
    }

    private bool ValidateEditSendShipping(SalesOrder salesOrder) {
      return salesOrder.Status == OrderStatus.Shipping;
    }



    private bool ValidateShowOrderData(QueryType queryType) {
      return queryType == QueryType.Sales || queryType == QueryType.SalesAuthorization || queryType == QueryType.SalesShipping;
    }

    private bool ValidateShowCreditEdtior(QueryType queryType) {
      return queryType == QueryType.SalesAuthorization;
    }

    private bool ValidateShowPickingEditor(QueryType queryType) {
      return queryType == QueryType.SalesPacking;
    }

    private bool ValidateShowPackingEditor(QueryType queryType) {
      return queryType == QueryType.SalesPacking;
    }

    private bool ValidateShippingEditor(QueryType queryType) {
      return queryType == QueryType.Sales  || queryType == QueryType.SalesShipping;
    }

    private bool ValidateSendShippingEditor(QueryType queryType) {
      return queryType == QueryType.Sales || queryType == QueryType.SalesShipping;
    }

    #endregion Private Methods

  } // class ActionsService

} // namespace Empiria.Trade.Sales

