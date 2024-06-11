/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Data Transfer Object                    *
*  Type     : OrderDto                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return money account actions.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.StateEnums;

namespace Empiria.Trade.Financial.Adapters {
  /// Output DTO used to return money account actions.
  public class MoneyAccountActionsDto {

    public MoneyAccountActionsDto() {
      // no-op
    }

    #region Public Properties

    public Boolean CanEdit {
      get; internal set;
    } = false;

    public Boolean CanDelete {
      get; internal set;
    } = false;

    public Boolean CanSuspend {
      get; internal set;
    } = false;

    public Boolean CanActivate {
      get; internal set;
    } = false;

    public Boolean CanSetPending {
      get; internal set;
    } = false;

    public Boolean CanEditTransactions {
      get; internal set;
    } = false;

    #endregion Public Properties

  

  } // internal class MoneyAccountActionsDto

} //namespace Empiria.Trade.Financial.Adapters
