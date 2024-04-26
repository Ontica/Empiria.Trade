/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : MoneyAccountMapper                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map money account.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.NetworkInformation;
using Empiria.StateEnums;


namespace Empiria.Trade.Financial.Adapters {
  /// Methods used to map money account.
  public class MoneyAccountMapper {

    static public MoneyAccountDto Map(MoneyAccount moneyAccount) {
      var dto = new MoneyAccountDto {
        MoneyAccountNumber = moneyAccount.Number,
        MoneyAccountType = moneyAccount.MoneyAccountType.Name,
        MoneyAccountOwner = moneyAccount.Owner.Name,
        MoneyAccountLimit = moneyAccount.CreditLimit,
        Balance = moneyAccount.GetDebit(),
        Status = ParseStatus(moneyAccount.Status),
        Transactions = MoneyAccountTransactionMapper.MapMoneyAccountTransactions(moneyAccount.MoneyAccountTransactions)
      };
      return dto;
    }

    static private string ParseStatus(EntityStatus moneyAccountStatus) {
      switch (moneyAccountStatus) {
        case EntityStatus.Suspended:
          return "Suspendido";
        case EntityStatus.Active:
          return "Activo";
        case EntityStatus.Pending:
          return "Pendiente";
        case EntityStatus.Deleted:
          return "Cancelado";

        default:
          return "Activo";
      }
    }


  } // class MoneyAccountMapper
} // namespace Empiria.Trade.Financial.Adapters
