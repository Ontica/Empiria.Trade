/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : MoneyAccountTransactionMapper              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map money account transaction item.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;


namespace Empiria.Trade.Financial.Adapters {
  /// Methods used to map money account transaction item. 
  static public class MoneyAccountTransactionItemMapper {

    static public MoneyAccountTransactionItemDto Map(MoneyAccountTransactionItem moneyAccountTransactionItem) {
      var dto = new MoneyAccountTransactionItemDto {
        UID = moneyAccountTransactionItem.UID,
        ItemType = moneyAccountTransactionItem.MoneyAccountTransactionItemType.MapToNamedEntity(),
        Reference = moneyAccountTransactionItem.ExtData,
        PaymentType = moneyAccountTransactionItem.PaymentType.MapToNamedEntity(),
        Deposit = moneyAccountTransactionItem.Deposit,
        Withdrawal = moneyAccountTransactionItem.Withdrawal,
        Notes = moneyAccountTransactionItem.Notes,
        PostedTime = moneyAccountTransactionItem.TransactionTime,
        Status = moneyAccountTransactionItem.MoneyAccountTransactionItemStatus,
      };
      return dto;
    }

    static public FixedList<MoneyAccountTransactionItemDto> MapMoneyAccountTransactionItems(FixedList<MoneyAccountTransactionItem> moneyAccountTransactionItems) {
      List<MoneyAccountTransactionItemDto> moneyAccountTransactionItemList = new List<MoneyAccountTransactionItemDto>();

      foreach (var transactionItem in moneyAccountTransactionItems) {
        moneyAccountTransactionItemList.Add(Map(transactionItem));
      }

      return moneyAccountTransactionItemList.ToFixedList();
    }

  } // class MoneyAccountTransactionItemMapper

} // namespace Empiria.Trade.Financial.Adapters
