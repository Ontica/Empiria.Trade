/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Mapper class                            *
*  Type     : MoneyAccountMapper                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Search MoneyAccounts.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Core.Common;
using System.Collections.Generic;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;


namespace Empiria.Trade.Financial.Adapters {
  /// Methods used to map Search MoneyAccounts. 
  public class SearchMoneyAccountMapper {

    #region Public methods

    static internal SearchMoneyAccountDto Map(SearchMoneyAccountFields query, FixedList<CreditMoneyAccount> moneyAccounts) {
      return new SearchMoneyAccountDto {
        Query = query,
        Columns = DataColumns(),
        Entries = MapMoneyAccounts(moneyAccounts)
      };
    }

    #endregion Public methods

    #region Private methods

    static private FixedList<DataTableColumn> DataColumns() {
      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("moneyAccountType", "No. Orden", "text-link"));
      columns.Add(new DataTableColumn("owner", "Cliente", "text"));
      columns.Add(new DataTableColumn("balance", "Saldo", "decimal"));
      columns.Add(new DataTableColumn("status", "Estatus", "text-tag"));
      
      return columns.ToFixedList();
    }

    static private FixedList<SearchMoneyAccountEntriesDto> MapMoneyAccounts(FixedList<CreditMoneyAccount> moneyAccounts) {
      List<SearchMoneyAccountEntriesDto> moneyAccountDtoList = new List<SearchMoneyAccountEntriesDto>();

      foreach (var moneyAccount in moneyAccounts) {
        moneyAccountDtoList.Add(MapMoneyAccount(moneyAccount));
      }

      return moneyAccountDtoList.ToFixedList();
    }

    static private SearchMoneyAccountEntriesDto MapMoneyAccount(CreditMoneyAccount moneyAccount) {
      var dto = new SearchMoneyAccountEntriesDto {
        UID = moneyAccount.UID,
        MoneyAccountType = "Credito a Cliente",
        Owner = GetOwnerName(moneyAccount.OwnerId),
        Balance = GetBalance(moneyAccount.OwnerId),
        Status = "Activo"
      };

      return dto;
    }

    static private string GetOwnerName(int ownerId) {
      var party = Party.Parse(ownerId);
      return party.Name;
    }

    static private decimal GetBalance(int ownerId) {
      var x = CreditMoneyAccount.ParseByOwnder(ownerId);

      return x.GetDebit();
    }

    #endregion Private methods






  } // class SearchMoneyAccount

} // namespace Empiria.Trade.Financial.Adapters 
