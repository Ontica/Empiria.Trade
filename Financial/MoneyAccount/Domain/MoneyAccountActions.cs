/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Actions Management             Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : MoneyAccountActions                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a money account actions.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;


namespace Empiria.Trade.Financial {
  /// Represents a money account actions.  
  public class MoneyAccountActions {

    #region Constructors and parsers

    public MoneyAccountActions() {
      //no-op 
    }

    #endregion Constructors and parsers

    #region Public properties

    public Boolean CanEdit {
      get; set;
    } = false;

    public Boolean CanDelete {
      get; set;
    } = false;

    public Boolean CanSuspend {
      get; set;
    } = false;

    public Boolean CanActivate {
      get; set;
    } = false;

    public Boolean CanSetPending {
      get; set;
    } = false;

    public Boolean CanEditTransactions {
      get; set;
    } = false;


    #endregion Public properties

    #region Public methods

    public void SetActions(MoneyAccount moneyAccount) {
      this.CanEdit = ValidateEdit(moneyAccount.Status);
      this.CanDelete = ValidateDelete(moneyAccount.Status);
      this.CanSuspend = ValidateSuspend(moneyAccount.Status);
      this.CanActivate = ValidateActive(moneyAccount.Status);
      this.CanSetPending = ValidateSetPending(moneyAccount.Status);
      this.CanEditTransactions = ValidateEditTransactions(moneyAccount.Status);
    }

    #endregion Public methods

    #region Private methods

    private bool ValidateEdit(EntityStatus status) {
      return (status == EntityStatus.Active);
    }

    private bool ValidateDelete(EntityStatus status) {
      return (status == EntityStatus.Active);
    }

    private bool ValidateSuspend(EntityStatus status) {
      return (status == EntityStatus.Active);
    }

    private bool ValidateActive(EntityStatus status) {
      return (status == EntityStatus.Suspended || status == EntityStatus.Pending);
    }

    private bool ValidateSetPending(EntityStatus status) {
      return (status == EntityStatus.Active);
    }

    private bool ValidateEditTransactions(EntityStatus status) {
      return (status == EntityStatus.Active);
    }


    #endregion Private methods

  } // Empiria.Trade.Financial

} // class MoneyAccountActions
