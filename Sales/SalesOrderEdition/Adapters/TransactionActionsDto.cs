/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Data Transfer Object                    *
*  Type     : OrderDto                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return order transaction actions.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


namespace Empiria.Trade.Sales.Adapters {

  // <summary>Output DTO used to return orders. </summary>
   public class TransactionActionsDto {

    public CanDto Can {
      get; internal set;
    }

    public ShowDto Show {
      get; internal set;
    }

  } // class TransactionActionDto

  //Class to set order permissions
  public class CanDto {

    public Boolean Apply{
      get; internal set;
    }

    public Boolean Authorize {
      get; internal set;
    }

    public Boolean Cancel {
      get; internal set;
    }

    public Boolean ClosePacking {
      get; internal set;
    }

    public Boolean Deauthorize {
      get; internal set;
    }

    public Boolean EditPicking {
      get; internal set;
    }

    public Boolean EditPacking {
      get; internal set;
    }

    public Boolean EditShipping {
      get; internal set;
    }

    public Boolean SendShipping {
      get; internal set;
    }

    public Boolean Update {
      get; internal set;
    }

  } // class CanDto

  // class to show or hide interface items
  public class ShowDto {

    public Boolean OrderData {
      get; internal set;
    }

    public Boolean CreditData {
      get; internal set;
    }

    public Boolean PickingData {
      get; internal set;
    }

    public Boolean PackingData {
      get; internal set;
    }

    public Boolean ShippingData {
      get; internal set;
    }

    public Boolean SendShippingData {
      get; internal set;
    }

  } //class ShowDto


} // namespace Empiria.Trade.Sales.Adapters
