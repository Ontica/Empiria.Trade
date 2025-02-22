/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Mapper class                            *
*  Type     : MoneyAccountActionsMapper                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map money account actions.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Trade.Financial.Adapters {
  ///  Methods used to map money account actions.
  public class MoneyAccountActionsMapper {

    static public MoneyAccountActionsDto Map(MoneyAccount moneyAccount) {

      MoneyAccountActions actions = new MoneyAccountActions();
      actions.SetActions(moneyAccount);

      var dto = new MoneyAccountActionsDto {
        CanEdit = actions.CanEdit,
        CanDelete = actions.CanDelete,
        CanSuspend = actions.CanSuspend,
        CanActivate = actions.CanActivate,
        CanSetPending = actions.CanSetPending,
        CanEditTransactions = actions.CanEditTransactions
      };
      return dto;
    }



  } // class MoneyAccountActionsMapper

} // namespace Empiria.Trade.Financial.Adapters
