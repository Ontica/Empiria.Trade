/* Empiria Trade *********************************************************************************************
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

using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;

namespace Empiria.Trade.Sales {

 

  /// <summary>Represents a sales order transaction actions. </summary>
  public class TransactionActions {

    

    #region Constructors and parsers

    public TransactionActions() {
     
    }



    #endregion Constructors and parsers

    #region Public properties

    public CanActions Can {
      get; set;
    } = new CanActions();

    public ShowActions Show {
      get; set;
    } = new ShowActions();

    #endregion Public properties

    public class CanActions {

      public Boolean Apply {
        get; set;
      } = false;

      public Boolean Authorize {
        get; set;
      } = false;

      public Boolean Cancel {
        get; set;
      } = false;

      public Boolean ClosePacking {
        get; set;
      } = false;

      public Boolean Deauthorize {
        get; set;
      } = false;

      public Boolean EditPicking {
        get; set;
      } = false;

      public Boolean EditPacking {
        get; set;
      } = false;

      public Boolean EditShipping {
        get; set;
      } = false;

      public Boolean SendShipping {
        get; set;
      } = false;

      public Boolean Update {
        get; set;
      } = false;

    }

    public class ShowActions {

      public Boolean OrderData {
        get; set;
      } = false;

      public Boolean CreditData {
        get; set;
      } = false;

      public Boolean PickingData {
        get; set;
      } = false;

      public Boolean PackingData {
        get; set;
      } = false;

      public Boolean ShippingData {
        get; set;
      } = false;

      public Boolean SendShippingData {
        get; set;
      } = false;
    }

    #region Public methods

    #endregion Public methods

    #region Private methods

    #endregion Internal methods

  } // class TransactionActions

}  // namespace Empiria.Trade.Sales.Order.Domain
