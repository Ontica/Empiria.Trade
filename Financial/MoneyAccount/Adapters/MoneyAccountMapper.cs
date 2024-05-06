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
        UID = moneyAccount.UID,
        MoneyAccountNumber = moneyAccount.Number,
        MoneyAccountType = moneyAccount.MoneyAccountType.MapToNamedEntity(),
        MoneyAccountOwner = moneyAccount.Owner.MapToNamedEntity(),
        MoneyAccountLimit = moneyAccount.CreditLimit,
        LimitDaysToPay = moneyAccount.DaysToPay,
        Notes = moneyAccount.Notes,
        Balance = moneyAccount.GetDebit(),
        Status = ParseStatus(moneyAccount.Status),
        Transactions = MoneyAccountTransactionMapper.MapMoneyAccountTransactions(moneyAccount.MoneyAccountTransactions)
      };
      return dto;
    }

    static private NamedEntityDto ParseStatus(EntityStatus moneyAccountStatus) {
      switch (moneyAccountStatus) {
        case EntityStatus.Suspended: 
         return new NamedEntityDto("Suspended", "Suspendido");
       case EntityStatus.Active:
          return new NamedEntityDto("Active", "Activo");
        case EntityStatus.Pending:
          return new NamedEntityDto("Pending","Pendiente");
        case EntityStatus.Deleted:
          return new NamedEntityDto("Deleted", "Cancelado");

        default:
          return new NamedEntityDto("Active", "Activo");
      }
    }


  } // class MoneyAccountMapper
} // namespace Empiria.Trade.Financial.Adapters
